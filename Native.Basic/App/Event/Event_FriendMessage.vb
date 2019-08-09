Imports Native.Basic.App.[Interface]
Imports Native.Basic.App.Model
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Native.Csharp.Sdk.Cqp.Core
Imports Native.Csharp.Sdk.Cqp.Enum
Imports Native.Csharp.Sdk.Cqp.Model
Imports Native.Csharp.Sdk.Cqp.Other

Namespace App.[Event]
	Public Class Event_FriendMessage
		Implements IEvent_FriendMessage

		''' <summary>
		''' Type=201 好友已添加<para/>
		''' 处理好友已经添加事件
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveFriendIncrease(ByVal sender As Object, ByVal e As FriendIncreaseEventArgs) Implements IEvent_FriendMessage.ReceiveFriendIncrease
            ' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            ' 这里处理消息


            e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=301 收到好友添加请求<para/>
		''' 处理收到的好友添加请求
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveFriendAddRequest(ByVal sender As Object, ByVal e As FriendAddRequestEventArgs) Implements IEvent_FriendMessage.ReceiveFriendAddRequest
			' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
			' 这里处理消息


			e.Handled = False   ' 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
		End Sub

		''' <summary>
		''' Type=21 好友消息<para/>
		''' 处理收到的好友消息
		''' </summary>
		''' <param name="sender">事件的触发对象</param>
		''' <param name="e">事件的附加参数</param>
		Public Sub ReceiveFriendMessage(ByVal sender As Object, ByVal e As PrivateMessageEventArgs) Implements IEvent_FriendMessage.ReceiveFriendMessage
            ' 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            ' 这里处理消息

            If e.FromQQ = Common.SuperQQ Then
                If e.Msg.ToLower() = "help" Then

                    CqApi.SendPrivateMessage(Common.SuperQQ, "start开始，y代表退出当前群，n代表不退出当前群！")
                ElseIf e.Msg.ToLower() = "start" Then
                    CqApi.SendPrivateMessage(Common.SuperQQ, "共有QQ群个数:" + QqGroupList.Count.ToString())
                    CqApi.SendPrivateMessage(Common.SuperQQ, "是否退出群：" + CurrentGroup.Name)
                ElseIf e.Msg.ToLower() = "y" Then

                    Dim result As Integer = DelGroup(CurrentGroup.Id)
                    If result >= 0 Then
                        Dim msg As String = "退出" + CurrentGroup.Name + CurrentGroup.Id.ToString() + "成功!"
                        CqApi.SendPrivateMessage(Common.SuperQQ, msg)
                        Common.GroupIndex += 1
                        CqApi.SendPrivateMessage(Common.SuperQQ, "是否退出群：" + CurrentGroup.Name)
                    Else
                        CqApi.SendPrivateMessage(Common.SuperQQ, "退出群：" + CurrentGroup.Name + "失败!失败代码=" + result.ToString())
                        Common.GroupIndex += 1
                        CqApi.SendPrivateMessage(Common.SuperQQ, "是否退出群：" + CurrentGroup.Name)
                    End If
                ElseIf e.Msg.ToLower() = "n" Then
                    CqApi.SendPrivateMessage(Common.SuperQQ, "没有退出群：" + CurrentGroup.Name)
                    Common.GroupIndex += 1
                    CqApi.SendPrivateMessage(Common.SuperQQ, "是否退出群：" + currentGroup.Name)
                End If
            End If
            'CqApi.SendPrivateMessage(e.FromQQ, CqApi.CqCode_At(e.FromQQ) & "你发送了这样的消息:" & e.Msg)


            e.Handled = True
			' e.Handled 相当于 原酷Q事件的返回值
			' 如果要回复消息，请调用api发送，并且置 true - 截断本条消息，不再继续处理 //注意：应用优先级设置为"最高"(10000)时， 不得置 true
			' 如果不回复消息，交由之后的应用/过滤器处理，这里置 false  - 忽略本条消息
		End Sub
	End Class
End Namespace
