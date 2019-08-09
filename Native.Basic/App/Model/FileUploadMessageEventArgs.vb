Imports Native.Csharp.Sdk.Cqp.Model
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace App.Model
	Public Class FileUploadMessageEventArgs
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
		''' 上传文件信息
		''' </summary>
		''' <returns></returns>
		Public Property File As GroupFile
	End Class
End Namespace
