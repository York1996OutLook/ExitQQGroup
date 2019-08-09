Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.IO.Compression
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports Native.Basic.Repair.Core
Imports Native.Basic.Repair.Enum

Namespace Repair.Helper
	Module AssemblyHelper
		Private preloaded As List(Of String) = New List(Of String)()

		Function ReadFromEmbeddedResources(ByVal assemblyNames As Dictionary(Of String, String), ByVal symbolNames As Dictionary(Of String, String), ByVal requestedAssemblyName As AssemblyName, ByVal executingAssembly As Assembly) As Assembly
			Dim text As String = requestedAssemblyName.Name.ToLowerInvariant()

			If requestedAssemblyName.CultureInfo IsNot Nothing AndAlso Not String.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name) Then
				text = requestedAssemblyName.CultureInfo.Name & "." & text
			End If

			Dim rawAssembly As Byte()

			Using stream As Stream = LoadStream(assemblyNames, text, executingAssembly)

				If stream Is Nothing Then
					Return Nothing
				End If

				rawAssembly = ReadStream(stream)
			End Using

			Using stream2 As Stream = LoadStream(symbolNames, text, executingAssembly)

				If stream2 IsNot Nothing Then
					Dim rawSymbolStore As Byte() = ReadStream(stream2)
					Return Assembly.Load(rawAssembly, rawSymbolStore)
				End If
			End Using

			Return Assembly.Load(rawAssembly)
		End Function

		Function ReadFromEmbeddedResources(ByVal name As String, ByVal executingAssembly As Assembly) As Assembly
			Dim assemblys As String() = executingAssembly.GetManifestResourceNames()
			Dim rawAssembly As Byte()

			For Each file In assemblys

				If file.EndsWith(".resources") Then

					If file = name.Split(","c)(0) Then
						Return executingAssembly
					End If
				End If

				If file = name Then
					Return executingAssembly
				End If

				If file.EndsWith(".dll") OrElse file.EndsWith(".dll.compressed") Then

					Using stream As Stream = LoadStream(file, executingAssembly)

						If stream Is Nothing Then
							Return Nothing
						End If

						rawAssembly = ReadStream(stream)
					End Using

					Dim tmp As Assembly = Assembly.Load(rawAssembly)

					If tmp.FullName = name Then
						Return tmp
					End If
				End If
			Next

			Return Nothing
		End Function

		Function CosturaAssemblyLoader(ByVal sender As Object, ByVal args As ResolveEventArgs, ByVal executingAssembly As Assembly) As Assembly
			Dim typeLoader As Type = executingAssembly.[GetType]("Costura.AssemblyLoader")

			If typeLoader IsNot Nothing Then

				If System.Environment.OSVersion.Version.Major > 5 AndAlso System.Environment.OSVersion.Version.Minor > 2 Then

					If preloaded.Any() AndAlso Not preloaded.Contains(executingAssembly.FullName) Then
						preloaded.Add(executingAssembly.FullName)
						CosturaPreload(executingAssembly, typeLoader)
					End If
				End If

				Dim assemblyNames As Dictionary(Of String, String) = ReflectionHelper.GetInstanceField(Of Dictionary(Of String, String))(typeLoader, Nothing, "assemblyNames")
				Dim symbolNames As Dictionary(Of String, String) = ReflectionHelper.GetInstanceField(Of Dictionary(Of String, String))(typeLoader, Nothing, "symbolNames")
				Dim assemblyName As AssemblyName = New AssemblyName(args.Name)
				Dim embeddedAssembly As Assembly = AssemblyHelper.ReadFromEmbeddedResources(assemblyNames, symbolNames, assemblyName, executingAssembly)

				If embeddedAssembly?.FullName = args.Name Then
					Return embeddedAssembly
				End If

				embeddedAssembly = ReflectionHelper.InvokeMethod(Of Assembly)(typeLoader, Nothing, "ResolveAssembly", New Object() {sender, args})

				If embeddedAssembly?.FullName = args.Name Then
					Return embeddedAssembly
				End If
			End If

			Return executingAssembly
		End Function

		Function AssemblyLoad(ByVal name As String, ByVal executingAssembly As Assembly) As Assembly
			If executingAssembly.FullName = name Then
				Return executingAssembly
			End If

			executingAssembly = CosturaAssemblyLoader(Nothing, New ResolveEventArgs(name), executingAssembly)

			If executingAssembly.FullName = name Then
				Return executingAssembly
			End If

			Dim tmp As Assembly = ReadFromEmbeddedResources(name, executingAssembly)

			If tmp IsNot Nothing Then
				Return tmp
			End If

			Return Nothing
		End Function

		Function IsDotNetAssembly(ByVal peFile As String) As Boolean
			Dim peHeader As UInteger
			Dim dataDictionaryStart As UShort
			Dim dataDictionaryRVA As UInteger() = New UInteger(15) {}
			Dim dataDictionarySize As UInteger() = New UInteger(15) {}
			Dim fs As Stream = New FileStream(peFile, FileMode.Open, FileAccess.Read)
			Dim reader As BinaryReader = New BinaryReader(fs)
			fs.Position = &H3C
			peHeader = reader.ReadUInt32()
			fs.Position = peHeader
			dataDictionaryStart = Convert.ToUInt16(Convert.ToUInt16(fs.Position) + &H60)
			fs.Position = dataDictionaryStart

			Try

				For i As Integer = 0 To 15 - 1
					dataDictionaryRVA(i) = reader.ReadUInt32()
					dataDictionarySize(i) = reader.ReadUInt32()
				Next

			Finally
				fs.Position = 0
				fs.Close()
			End Try

			Return dataDictionaryRVA(14) = 0
		End Function

		Sub PreloadUnmanagedLibraries(ByVal executingAssembly As Assembly, ByVal hash As String, ByVal tempBasePath As String, ByVal libs As List(Of String), ByVal checksums As Dictionary(Of String, String))
			Dim mutexId = $"Costura{hash}"

			Using mutex = New Mutex(False, mutexId)
				Dim hasHandle = False

				Try

					Try
						hasHandle = mutex.WaitOne(60000, False)

						If hasHandle = False Then
							Throw New TimeoutException("Timeout waiting for exclusive access")
						End If

					Catch __unusedAbandonedMutexException1__ As AbandonedMutexException
						hasHandle = True
					End Try

					Dim bittyness = If(IntPtr.Size = 8, "64", "32")
					CreateDirectory(Path.Combine(tempBasePath, bittyness))
					InternalPreloadUnmanagedLibraries(executingAssembly, tempBasePath, libs, checksums)
				Finally

					If hasHandle Then
						mutex.ReleaseMutex()
					End If
				End Try
			End Using
		End Sub

		Function CalculateChecksum(ByVal filename As String) As String
			Using fs = New FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite Or FileShare.Delete)

				Using bs = New BufferedStream(fs)

					Using sha1 = New SHA1CryptoServiceProvider()
						Dim hash = sha1.ComputeHash(bs)
						Dim formatted = New StringBuilder(2 * hash.Length)

						For Each b In hash
							formatted.AppendFormat("{0:X2}", b)
						Next

						Return formatted.ToString()
					End Using
				End Using
			End Using
		End Function

		Sub CopyTo(ByVal source As Stream, ByVal destination As Stream)
			'Dim array As Byte() = New Byte(81919) {}
			'Dim count As Integer

			source.CopyTo(destination)

			'While CSharpImpl.__Assign(count, source.Read(array, 0, array.Length)) > 0
			'    destination.Write(array, 0, count)
			'End While
		End Sub

		Function ReadStream(ByVal stream As Stream) As Byte()
			Dim array As Byte() = New Byte(stream.Length - 1) {}
			stream.Read(array, 0, array.Length)
			Return array
		End Function

		Function LoadStream(ByVal fullName As String, ByVal executingAssembly As Assembly) As Stream
			If fullName.EndsWith(".compressed") Then

				Using manifestResourceStream As Stream = executingAssembly.GetManifestResourceStream(fullName)

					Using deflateStream As DeflateStream = New DeflateStream(manifestResourceStream, CompressionMode.Decompress)
						Dim memoryStream As MemoryStream = New MemoryStream()
						CopyTo(deflateStream, memoryStream)
						memoryStream.Position = 0L
						Return memoryStream
					End Using
				End Using
			End If

			Return executingAssembly.GetManifestResourceStream(fullName)
		End Function

		Function LoadStream(ByVal resourceNames As Dictionary(Of String, String), ByVal name As String, ByVal executingAssembly As Assembly) As Stream
			Dim fullName As String = ""

			If resourceNames.TryGetValue(name, fullName) Then
				Return LoadStream(fullName, executingAssembly)
			End If

			Return Nothing
		End Function

		Private Sub CosturaPreload(ByVal executingAssembly As Assembly, ByVal typeLoader As Type)
			Dim preloadList As List(Of String) = New List(Of String)()
			Dim preload32List As List(Of String) = New List(Of String)()
			Dim checksums As Dictionary(Of String, String) = New Dictionary(Of String, String)()

			Try
				checksums = ReflectionHelper.GetInstanceField(Of Dictionary(Of String, String))(typeLoader, Nothing, "checksums")
				preload32List = ReflectionHelper.GetInstanceField(Of List(Of String))(typeLoader, Nothing, "preload32List")
				preloadList = ReflectionHelper.GetInstanceField(Of List(Of String))(typeLoader, Nothing, "preloadList")
			Catch
			End Try

			If checksums.Any() Then
				Dim hash = $"{Process.GetCurrentProcess().Id}_{executingAssembly.GetHashCode()}"
				Dim prefixPath = Path.Combine(Path.GetTempPath(), "Costura")
				Dim tempBasePath = Path.Combine(prefixPath, hash)
				PreloadUnmanagedLibraries(executingAssembly, hash, tempBasePath, preloadList.Concat(preload32List).ToList(), checksums)
			End If
		End Sub

		Private Sub CreateDirectory(ByVal tempBasePath As String)
			If Not Directory.Exists(tempBasePath) Then
				Directory.CreateDirectory(tempBasePath)
			End If
		End Sub

		Private Function ResourceNameToPath(ByVal [lib] As String) As String
			Dim bittyness = If(IntPtr.Size = 8, "64", "32")
			Dim name = [lib]

			If [lib].StartsWith(String.Concat("costura", bittyness, ".")) Then
				name = Path.Combine(bittyness, [lib].Substring(10))
			ElseIf [lib].StartsWith("costura.") Then
				name = [lib].Substring(8)
			End If

			If name.EndsWith(".compressed") Then
				name = name.Substring(0, name.Length - 11)
			End If

			Return name
		End Function

		Private Sub InternalPreloadUnmanagedLibraries(ByVal executingAssembly As Assembly, ByVal tempBasePath As String, ByVal libs As IList(Of String), ByVal checksums As Dictionary(Of String, String))
			Dim name As String

			For Each [lib] In libs
				name = ResourceNameToPath([lib])
				Dim assemblyTempFilePath = Path.Combine(tempBasePath, name)

				If File.Exists(assemblyTempFilePath) Then
					Dim checksum = CalculateChecksum(assemblyTempFilePath)

					If checksum <> checksums([lib]) Then
						File.Delete(assemblyTempFilePath)
					End If
				End If

				If Not File.Exists(assemblyTempFilePath) Then

					Using copyStream = LoadStream([lib], executingAssembly)

						Using assemblyTempFile = File.OpenWrite(assemblyTempFilePath)
							CopyTo(copyStream, assemblyTempFile)
						End Using
					End Using
				End If
			Next

			Kernel32.AddDllDirectory(tempBasePath)
			Dim errorModes As UInteger = 32771
			Dim originalErrorMode = Kernel32.SetErrorMode(errorModes)

			For Each [lib] In libs
				name = ResourceNameToPath([lib])

				If name.EndsWith(".dll") Then
					Dim assemblyTempFilePath = Path.Combine(tempBasePath, name)
					Kernel32.LoadLibraryEx(assemblyTempFilePath, IntPtr.Zero, LoadLibraryFlags.LOAD_LIBRARY_SEARCH_USER_DIRS)
				End If
			Next

			Kernel32.SetErrorMode(originalErrorMode)
		End Sub
	End Module
End Namespace
