Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace App.Model
	Public Class GroupManageAlterEventArgs
		Inherits EventArgsBase

		''' <summary>
		''' 发送时间
		''' </summary>
		''' <returns></returns>
		Public Property SendTime As DateTime

		''' <summary>
		''' 来源群号
		''' </summary>
		''' <returns></returns>
		Public Property FromGroup As Long

		''' <summary>
		''' 被操作QQ
		''' </summary>
		''' <returns></returns>
		Public Property BeingOperateQQ As Long
	End Class
End Namespace
