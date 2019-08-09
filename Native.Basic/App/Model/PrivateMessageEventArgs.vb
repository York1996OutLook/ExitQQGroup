Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace App.Model
	Public Class PrivateMessageEventArgs
		Inherits EventArgsBase

		''' <summary>
		''' 消息ID
		''' </summary>
		''' <returns></returns>
		Public Property MsgId As Integer

		''' <summary>
		''' 消息内容
		''' </summary>
		''' <returns></returns>
		Public Property Msg As String
	End Class
End Namespace
