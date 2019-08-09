Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.ExceptionServices
Imports System.Text
Imports Unity
Imports Native.Basic.App.Core
Imports Native.Basic.App.[Interface]
Imports Native.Basic.App.Model
Imports Native.Csharp.Sdk.Cqp

Namespace App.[Event]

	Public Class Event_AppMain

		''' <summary>
		''' 回调注册
		''' </summary>
		''' <param name="container"></param>
		Public Shared Sub Registbackcall(ByVal container As IUnityContainer)

			' 当需要注册自己的回调类型时
			' 在此写上需要注册的回调类型, 以 <接口, 实现类> 的方式进行注册, 同时需要给所有注入的类进行命名
			' 
			' 如果注入时没有提供名称, 则 SDK 在分发事件时将无法获取到对应的类型的实例!!!
			' 如果注入时没有提供名称, 则 SDK 在分发事件时将无法获取到对应的类型的实例!!!
			' 如果注入时没有提供名称, 则 SDK 在分发事件时将无法获取到对应的类型的实例!!!
			' 重要的事情说三遍!!!
			' 
			' 下列代码演示的是如何将 SDK 预置的实现类注入到容器中
#Region "--回调注入--"
			container.RegisterType(Of IEvent_AppStatus, Event_AppStatus)("Default_AppStatus")
			container.RegisterType(Of IEvent_DiscussMessage, Event_DiscussMessage)("Default_DiscussMessage")
			container.RegisterType(Of IEvent_FriendMessage, Event_FriendMessage)("Default_FriendMessage")
			container.RegisterType(Of IEvent_GroupMessage, Event_GroupMessage)("Default_GroupMessage")
			container.RegisterType(Of IEvent_OtherMessage, Event_OtherMessage)("Default_OtherMessage")
#End Region

			' 当需要新注册回调类型时
			' 在此写上需要注册的回调类型, 以 <接口, 实现类> 的方式进行注册
			' 下列代码演示的是如何将 IEvent_UserExpand 的实现类 Event_UserExpand 类注入到容器中
			container.RegisterType(Of IEvent_UserExpand, Event_UserExpand)()
		End Sub

		''' <summary>
		''' 回调分发
		''' </summary>
		''' <param name="container"></param>
		Public Shared Sub Resolvebackcall(ByVal container As IUnityContainer)

			' 当已经注入了新的回调类型时
			' 在此分发已经注册的回调类型, 解析完毕后分发到导出的事件进行注册
			' 下列代码演示如何将 IEvent_UserExpand 接口实例化并拿到对应的实例
			Dim userExpand As IEvent_UserExpand = container.Resolve(Of IEvent_UserExpand)()
			AddHandler UserExport.UserOpenConsole, AddressOf userExpand.OpenConsoleWindow
		End Sub
	End Class
End Namespace
