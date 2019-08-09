Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Threading.Tasks
Imports System.Runtime.CompilerServices

Namespace Repair.Helper
	Module ReflectionHelper
		Public bindFlags As BindingFlags = BindingFlags.Instance Or BindingFlags.[Public] Or BindingFlags.NonPublic Or BindingFlags.[Static]

		Function InvokeMethod(Of T)(ByVal type As Type, ByVal instance As Object, ByVal methodName As String, ParamArray args As Object()) As T
			Dim method = GetMethod(type, methodName)
			Return CType(method?.Invoke(instance, args), T)
		End Function

		<Extension()>
		Function InvokeMethod(Of T)(ByVal obj As T, ByVal methodName As String, ParamArray args As Object()) As T
			Dim type = GetType(T)
			Dim method = GetMethod(type, methodName)
			Return CType(method?.Invoke(obj, args), T)
		End Function

		Function GetMethod(ByVal type As Type, ByVal methodName As String) As MethodInfo
			Return If(type.GetMethods().Where(Function(mi) mi.Name = methodName).FirstOrDefault(), Nothing)
		End Function

		Function GetInstanceField(Of T)(ByVal type As Type, ByVal instance As Object, ByVal fieldName As String) As T
			Dim field As FieldInfo = type.GetField(fieldName, bindFlags)
			Return CType(field.GetValue(instance), T)
		End Function

		Sub SetInstanceField(Of T)(ByVal type As Type, ByVal instance As Object, ByVal fieldName As String, ByVal fieldValue As T)
			Dim field As FieldInfo = type.GetField(fieldName, bindFlags)
			field.SetValue(instance, fieldValue)
		End Sub

		<Extension()>
		Sub ClearEventInvocations(ByVal obj As Object, ByVal eventName As String)
			Dim fi = obj.[GetType]().GetEventField(eventName)
			If fi Is Nothing Then Return
			fi.SetValue(obj, Nothing)
		End Sub

		<Extension()>
		Function GetEventField(ByVal type As Type, ByVal eventName As String) As FieldInfo
			Dim field As FieldInfo = Nothing

			While type IsNot Nothing
				field = type.GetField(eventName, bindFlags)
				If field IsNot Nothing AndAlso (field.FieldType = GetType(MulticastDelegate) OrElse field.FieldType.IsSubclassOf(GetType(MulticastDelegate))) Then Exit While
				field = type.GetField("EVENT_" & eventName.ToUpper(), bindFlags)
				If field IsNot Nothing Then Exit While
				type = type.BaseType
			End While

			Return field
		End Function
	End Module
End Namespace
