Imports Native.Basic.App.[Interface]
Imports Native.Basic.App.Model
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace App.[Event]
	Public Class Event_DiscussMessage
		Implements IEvent_DiscussMessage

		''' <summary>
		''' Type=4 收到讨论组消息 <para/>
		''' 处理收到的讨论组消息
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveDiscussMessage(ByVal sender As Object, ByVal e As DiscussMessageEventArgs) Implements IEvent_DiscussMessage.ReceiveDiscussMessage
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=21 讨论组私聊消息 <para/>
		''' 处理收到的讨论组私聊消息
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveDiscussPrivateMessage(ByVal sender As Object, ByVal e As PrivateMessageEventArgs) Implements IEvent_DiscussMessage.ReceiveDiscussPrivateMessage
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub
	End Class
End Namespace
