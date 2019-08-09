Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Native.Basic.App.Model

Namespace App.[Interface]
	''' <summary>
	''' 酷Q 讨论组消息接口
	''' </summary>
	Public Interface IEvent_DiscussMessage

		''' <summary>
		''' Type=4 讨论组消息 <para/>
		''' 当在派生类中重写时, 处理收到的讨论组消息
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveDiscussMessage(ByVal sender As Object, ByVal e As DiscussMessageEventArgs)

		''' <summary>
		''' Type=21 讨论组私聊消息 <para/>
		''' 当在派生类中重写时, 处理收到的讨论组私聊消息
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveDiscussPrivateMessage(ByVal sender As Object, ByVal e As PrivateMessageEventArgs)
	End Interface
End Namespace
