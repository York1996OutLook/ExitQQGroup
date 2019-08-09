Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace App.Model

	Public Class DiscussMessageEventArgs
		Inherits EventArgsBase

		''' <summary>
		''' 消息Id
		''' </summary>
		''' <returns></returns>
		Public Property MsgId As Integer

		''' <summary>
		''' 来源讨论组
		''' </summary>
		''' <returns></returns>
		Public Property FromDiscuss As Long

		''' <summary>
		''' 消息内容
		''' </summary>
		''' <returns></returns>
		Public Property Msg As String
	End Class
End Namespace
