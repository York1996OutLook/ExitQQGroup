Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text
Imports Native.Basic.Repair.Enum

Namespace Repair.Core
	Module Kernel32
		<DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
		Function AddDllDirectory(ByVal lpPathName As String) As <MarshalAs(UnmanagedType.Bool)> Boolean
		End Function

		<DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
		Function SetDllDirectory(ByVal lpPathName As String) As <MarshalAs(UnmanagedType.Bool)> Boolean
		End Function

		<DllImport("kernel32.dll")>
		Function SetErrorMode(ByVal uMode As UInteger) As UInteger
		End Function
		<DllImport("kernel32", SetLastError:=True, CharSet:=CharSet.Unicode)>
		Function LoadLibrary(ByVal dllToLoad As String) As IntPtr
		End Function
		<DllImport("kernel32")>
		Function LoadLibraryEx(lpFileName As String, hFile As IntPtr, dwFlags As LoadLibraryFlags) As IntPtr
		End Function

	End Module
End Namespace
