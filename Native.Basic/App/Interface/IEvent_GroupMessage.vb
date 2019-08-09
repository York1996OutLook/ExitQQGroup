Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Native.Basic.App.Model

Namespace App.[Interface]
	''' <summary>
	''' 酷Q 群消息接口
	''' </summary>
	Public Interface IEvent_GroupMessage

		''' <summary>
		''' Type=2 群消息<para/>
		''' 当在派生类中重写时, 处理收到的群消息
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveGroupMessage(ByVal sender As Object, ByVal e As GroupMessageEventArgs)

		''' <summary>
		''' Type=21 群私聊<para/>
		''' 当在派生类中重写时, 处理收到的群私聊消息
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveGroupPrivateMessage(ByVal sender As Object, ByVal e As PrivateMessageEventArgs)

		''' <summary>
		''' Type=11 群文件上传事件<para/>
		''' 当在派生类中重写时, 处理收到的群文件上传结果
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveGroupFileUpload(ByVal sender As Object, ByVal e As FileUploadMessageEventArgs)

		''' <summary>
		''' Type=101 群事件 - 管理员增加<para/>
		''' 当在派生类中重写时, 处理收到的群管理员增加事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveGroupManageIncrease(ByVal sender As Object, ByVal e As GroupManageAlterEventArgs)

		''' <summary>
		''' Type=101 群事件 - 管理员减少<para/>
		''' 当在派生类中重写时, 处理收到的群管理员减少事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveGroupManageDecrease(ByVal sender As Object, ByVal e As GroupManageAlterEventArgs)

		''' <summary>
		''' Type=103 群事件 - 群成员增加 - 主动入群<para/>
		''' 当在派生类中重写时, 处理收到的群成员增加 (主动入群) 事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveGroupMemberJoin(ByVal sender As Object, ByVal e As GroupMemberAlterEventArgs)

		''' <summary>
		''' Type=103 群事件 - 群成员增加 - 被邀入群<para/>
		''' 当在派生类中重写时, 处理收到的群成员增加 (被邀入群) 事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveGroupMemberInvitee(ByVal sender As Object, ByVal e As GroupMemberAlterEventArgs)

		''' <summary>
		''' Type=102 群事件 - 群成员减少 - 成员离开<para/>
		''' 当在派生类中重写时, 处理收到的群成员减少 (成员离开) 事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveGroupMemberLeave(ByVal sender As Object, ByVal e As GroupMemberAlterEventArgs)

		''' <summary>
		''' Type=102 群事件 - 群成员减少 - 成员移除<para/>
		''' 当在派生类中重写时, 处理收到的群成员减少 (成员移除) 事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveGroupMemberRemove(ByVal sender As Object, ByVal e As GroupMemberAlterEventArgs)

		''' <summary>
		''' Type=302 群事件 - 群请求 - 申请入群<para/>
		''' 当在派生类中重写时, 处理收到的群请求 (申请入群) 事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveGroupAddApply(ByVal sender As Object, ByVal e As GroupAddRequestEventArgs)

		''' <summary>
		''' Type=302 群事件 - 群请求 - 被邀入群 (机器人被邀)<para/>
		''' 当在派生类中重写时, 处理收到的群请求 (被邀入群) 事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Sub ReceiveGroupAddInvitee(ByVal sender As Object, ByVal e As GroupAddRequestEventArgs)
	End Interface
End Namespace
