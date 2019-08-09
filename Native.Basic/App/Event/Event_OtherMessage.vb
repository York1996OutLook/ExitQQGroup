Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Native.Basic.App.Model
Imports Native.Basic.App.[Interface]
Imports Native.Csharp.Sdk.Cqp

Namespace App.[Event]
	Public Class Event_OtherMessage
		Implements IEvent_OtherMessage

		''' <summary>
		''' Type=21 在线状态消息<para/>
		''' 处理收到的在线状态消息
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveOnlineStatusMessage(ByVal sender As Object, ByVal e As PrivateMessageEventArgs) Implements IEvent_OtherMessage.ReceiveOnlineStatusMessage
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息

			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub
	End Class
End Namespace
