Namespace App.Model

	Public Class EventArgsBase
		Inherits EventArgs

		''' <summary>
		''' 来源QQ
		''' </summary>
		''' <returns></returns>
		Public Property FromQQ As Long

		''' <summary>
		''' 获取或设置一个值, 该值只是是否处理过此事件
		''' </summary>
		''' <returns></returns>
		Public Property Handled As Boolean
	End Class
End Namespace
