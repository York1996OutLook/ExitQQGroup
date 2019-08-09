Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Native.Basic.App.Model
Imports Native.Basic.App.[Interface]
Imports Native.Csharp.Sdk.Cqp
Imports Native.Csharp.Sdk.Cqp.Model

Namespace App.[Event]
	Public Class Event_GroupMessage
		Implements IEvent_GroupMessage

		''' <summary>
		''' Type=2 群消息<para/>
		''' 处理收到的群消息
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveGroupMessage(ByVal sender As Object, ByVal e As GroupMessageEventArgs) Implements IEvent_GroupMessage.ReceiveGroupMessage
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息

			If e.FromAnonymous IsNot Nothing Then   ' 如果此属性不为 Nothing, 则消息来自于匿名成员
                'CqApi.SendGroupMessage(e.FromGroup, e.FromAnonymous.CodeName & " 你发送了这样的消息: " & e.Msg)
                e.Handled = True
				Return      ' 因为 e.Handled = true 只是起到标识作用, 因此还需要手动返回
			End If

			' 与2019年02月26日默认注释此行
			' CqApi.SendGroupMessage(e.FromGroup, CqApi.CqCode_At(e.FromQQ) & "你发送了这样的消息: " & e.Msg)

			e.Handled = True    ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=21 群私聊<para/>
		''' 处理收到的群私聊消息
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveGroupPrivateMessage(ByVal sender As Object, ByVal e As PrivateMessageEventArgs) Implements IEvent_GroupMessage.ReceiveGroupPrivateMessage
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=11 群文件上传事件<para/>
		''' 处理收到的群文件上传结果
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveGroupFileUpload(ByVal sender As Object, ByVal e As FileUploadMessageEventArgs) Implements IEvent_GroupMessage.ReceiveGroupFileUpload
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=101 群事件 - 管理员增加<para/>
		''' 处理收到的群管理员增加事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveGroupManageIncrease(ByVal sender As Object, ByVal e As GroupManageAlterEventArgs) Implements IEvent_GroupMessage.ReceiveGroupManageIncrease
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=101 群事件 - 管理员减少<para/>
		''' 处理收到的群管理员减少事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveGroupManageDecrease(ByVal sender As Object, ByVal e As GroupManageAlterEventArgs) Implements IEvent_GroupMessage.ReceiveGroupManageDecrease
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=103 群事件 - 群成员增加 - 主动入群<para/>
		''' 处理收到的群成员增加 (主动入群) 事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveGroupMemberJoin(ByVal sender As Object, ByVal e As GroupMemberAlterEventArgs) Implements IEvent_GroupMessage.ReceiveGroupMemberJoin
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=103 群事件 - 群成员增加 - 被邀入群<para/>
		''' 处理收到的群成员增加 (被邀入群) 事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveGroupMemberInvitee(ByVal sender As Object, ByVal e As GroupMemberAlterEventArgs) Implements IEvent_GroupMessage.ReceiveGroupMemberInvitee
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=102 群事件 - 群成员减少 - 成员离开<para/>
		''' 处理收到的群成员减少 (成员离开) 事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveGroupMemberLeave(ByVal sender As Object, ByVal e As GroupMemberAlterEventArgs) Implements IEvent_GroupMessage.ReceiveGroupMemberLeave
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=102 群事件 - 群成员减少 - 成员移除<para/>
		''' 处理收到的群成员减少 (成员移除) 事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveGroupMemberRemove(ByVal sender As Object, ByVal e As GroupMemberAlterEventArgs) Implements IEvent_GroupMessage.ReceiveGroupMemberRemove
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=302 群事件 - 群请求 - 申请入群<para/>
		''' 处理收到的群请求 (申请入群) 事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveGroupAddApply(ByVal sender As Object, ByVal e As GroupAddRequestEventArgs) Implements IEvent_GroupMessage.ReceiveGroupAddApply
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=302 群事件 - 群请求 - 被邀入群 (机器人被邀)<para/>
		''' 处理收到的群请求 (被邀入群) 事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveGroupAddInvitee(ByVal sender As Object, ByVal e As GroupAddRequestEventArgs) Implements IEvent_GroupMessage.ReceiveGroupAddInvitee
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub
	End Class
End Namespace
