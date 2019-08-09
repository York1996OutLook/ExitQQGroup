Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Native.Basic.App.Model

Namespace App.[Interface]
	''' <summary>
	''' 酷Q 好友消息接口
	''' </summary>
	Public Interface IEvent_FriendMessage

		''' <summary>
		''' Type=201 好友已添加<para/>
		''' 当在派生类中重写时, 处理好友已经添加事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveFriendIncrease(ByVal sender As Object, ByVal e As FriendIncreaseEventArgs)

		''' <summary>
		''' Type=301 收到好友添加请求<para/>
		''' 当在派生类中重写时, 处理收到的好友添加请求
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveFriendAddRequest(ByVal sender As Object, ByVal e As FriendAddRequestEventArgs)

		''' <summary>
		''' Type=21 好友消息<para/>
		''' 当在派生类中重写时, 处理收到的好友消息
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveFriendMessage(ByVal sender As Object, ByVal e As PrivateMessageEventArgs)
	End Interface
End Namespace
