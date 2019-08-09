Imports System.IO
Imports System.Reflection
Imports Native.Basic.Repair.Helper

Public Class ModuleInitializer

	Public Shared Sub Initialize()
		' 注册程序集加载失败事件, 用于 Fody 库重定向的补充
		AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CurrentDomain_AssemblyResolve
	End Sub

	''' <summary>
	''' 依赖库加载失败事件, 用于重定向到本项目下加载
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="args"></param>
	''' <returns></returns>
	Private Shared Function CurrentDomain_AssemblyResolve(ByVal sender As Object, ByVal args As ResolveEventArgs) As Assembly
        If args.Name.Split(","c)(0).EndsWith(".resources") Then
            Return Nothing
        End If

        Dim loadAssembly As Assembly() = AppDomain.CurrentDomain.GetAssemblies()
		Dim assembly As Assembly = loadAssembly.Where(Function(w) w.FullName.CompareTo(args.Name) = 0).LastOrDefault()

		If assembly IsNot Nothing Then
			Return assembly
		End If

		If IsNothing(args.RequestingAssembly) Then
			assembly = Assembly.GetExecutingAssembly()
		Else
			assembly = args.RequestingAssembly
		End If

		If String.IsNullOrEmpty(assembly.Location) Then
			Dim uri As Uri = New Uri(assembly.CodeBase)

			If uri.IsFile Then

				If File.Exists(uri.LocalPath) Then
					assembly = Assembly.LoadFile(uri.LocalPath)
				End If
			End If
		End If

		If args.RequestingAssembly IsNot Nothing Then
			Dim tmp As Assembly = AssemblyHelper.AssemblyLoad(args.Name, assembly)

			If tmp IsNot Nothing Then
				Return tmp
			End If
		End If

		Dim uriOuter As Uri = New Uri(If(assembly.Location Is Nothing, assembly.CodeBase, assembly.Location))

		If Not String.IsNullOrEmpty(uriOuter.LocalPath) AndAlso uriOuter.IsFile Then
			Dim paths As Queue(Of String) = New Queue(Of String)()
			Dim path As String = System.IO.Path.GetDirectoryName(uriOuter.LocalPath)

			If Directory.Exists(path) Then

				For Each f In Directory.GetFiles(path)

					If AssemblyHelper.IsDotNetAssembly(f) Then
						paths.Enqueue(f)
					End If
				Next
			End If

			Dim bin As String = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "bin")

			If Directory.Exists(bin) Then

				For Each f In Directory.GetFiles(bin, "*.dll")

					If AssemblyHelper.IsDotNetAssembly(f) Then
						paths.Enqueue(f)
					End If
				Next
			End If

			For Each file In paths

				If System.IO.File.Exists(file) Then

					Try
						Dim assemblyName As AssemblyName = AssemblyName.GetAssemblyName(file)
						Dim tmp As Assembly = Assembly.LoadFile(file)
						tmp = AssemblyHelper.AssemblyLoad(args.Name, tmp)

						If tmp IsNot Nothing Then
							Return tmp
						End If

					Catch
					End Try
				End If
			Next
		End If

		If assembly.FullName Is args.Name Then
			Return assembly
		End If

		If args.RequestingAssembly Is Assembly.GetExecutingAssembly() Then
			Return Nothing
		End If

		Return args.RequestingAssembly
	End Function

End Class
