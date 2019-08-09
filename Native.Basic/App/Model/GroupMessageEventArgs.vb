Imports Native.Csharp.Sdk.Cqp.Model
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace App.Model
	Public Class GroupMessageEventArgs
		Inherits EventArgsBase

		''' <summary>
		''' 消息Id
		''' </summary>
		''' <returns></returns>
		Public Property MsgId As Integer

		''' <summary>
		''' 来源群号
		''' </summary>
		''' <returns></returns>
		Public Property FromGroup As Long

		''' <summary>
		''' 是否是匿名消息
		''' </summary>
		''' <returns></returns>
		Public Property IsAnonymousMsg As Boolean

		''' <summary>
		''' 来源匿名
		''' </summary>
		''' <returns></returns>
		Public Property FromAnonymous As GroupAnonymous

		''' <summary>
		''' 消息内容
		''' </summary>
		''' <returns></returns>
		Public Property Msg As String
	End Class
End Namespace
