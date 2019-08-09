Imports Native.Csharp.Sdk.Cqp
Imports Unity
Imports Native.Csharp.Sdk.Cqp.Model
Namespace App
    ''' <summary>
    ''' 用于存放 App 数据的公共类
    ''' </summary>
    Public Module Common

        ''' <summary>
        ''' 获取或设置 App 在运行期间所使用的数据路径
        ''' </summary>
        ''' <returns></returns>
        Public Property AppDirectory As String

        ''' <summary>
        ''' 获取或设置当前 App 是否处于运行状态
        ''' </summary>
        ''' <returns></returns>
        Public Property IsRunning As Boolean

        ''' <summary>
        ''' 获取或设置当前 App 使用的 <see cref="Csharp.Sdk.Cqp.CqApi"/> 接口实例
        ''' </summary>
        ''' <returns></returns>
        Public Property CqApi As CqApi

        ''' <summary>
        ''' 获取或设置当前 App 使用的依赖注入容器实例
        ''' </summary>
        ''' <returns></returns>
        Public Property UnityContainer As IUnityContainer
        Public Property QqGroupList As List(Of Csharp.Sdk.Cqp.Model.Group)
        'Public Property SuperQQ As Long = 3493143959
        Public Property SuperQQ As Long = 603997262
        Public ReadOnly Property CurrentGroup() As Group
            Get
                Return QqGroupList(GroupIndex)
            End Get

        End Property
        Public Function GetGroupTitle(groupId As Long) As String

            Dim groupTitle As String = ""
            For Each group As Group In Common.QqGroupList
                If group.Id = groupId Then
                    groupTitle = group.Name
                    Exit For
                End If
            Next
            Return groupTitle
        End Function
        Public Property GroupIndex = 0
        Public Function DelGroup(groupId As Long) As Integer
            Return CqApi.SetGroupExit(groupId)
        End Function
    End Module

End Namespace
