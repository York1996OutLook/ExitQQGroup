## Native.SDK 优点介绍

> 1. 程序集脱库打包
> 2. 类UWP开发体验
> 3. 完美翻译酷QApi
> 4. 支持酷Q应用打包
> 5. 支持附加进程调试

## Native.SDK 项目结构

![SDK结构](https://github.com/Jie2GG/Image/blob/master/NativeSDK(1).png "SDK结构") <br/>

## Native.SDK 开发环境

>1. Visual Studio 2012 或更高版本
>2. Microsoft .Net Framework 4.0 **(XP系统支持的最后一个版本)**

## Native.SDK 部署流程

	1. 下载并打开 Native.SDK
	2. 打开 Native.Basic 项目属性, 修改 "应用程序" 中的 "程序集名称" 为你的AppId(规则参见http://d.cqp.me/Pro/开发/基础信息)
	3. 展开 Native.Basic 项目, 修改 "Native.Basic.json" 文件名为你的AppId
	4. 展开 Native.Basic 项目, 找到 App -> Core -> LibExport.tt 文件, 右击选择 "运行自定义工具"
	
	此时 Native.SDK 的开发环境已经配置成功!
	要找到生成的 程序集, 请找 Native.Basic -> bin -> x86 -> (Debug\Release) 

## Native.SDK 调试流程

    1. 打开 酷Q Air/Pro, 并且登录机器人账号
    2. 打开 Native.Basic 项目, 修改 "生成" 中的 "输出路径" 为 酷Q的 "dev" 路径
    3. 重新生成 Native.Basic 项目
    4. 在酷Q上使用 "重载应用" 功能, 重载所有应用
    5. 依次选择VS的菜单项: "调试" -> "附加到进程"
    6. 选择 CQA.exe/CQP.exe 的托管进程, 选择附加
    7. 附加成功后进入调试模式, 即可进行断点 (注: 仅在只加载一个 .Net 应用的酷Q可以进行调试)

## Native.SDK 已知问题

> 1. ~~对于 VisualBasic 项目不知道为什么安装高版本的 Fody 就编译不通过, 现 Fody 版本为 1.6.2, 所以暂时不支持无缝升级到 .Net Framewrok 4.5+~~(提供了4.5版本)

## Native.SDK 更新日志
> 2019年06月07日 版本: V2.0.6.0607

	由于 酷Q 停止对 Windows XP/Vista 系统的支持, 所以 Native.SDK 将停止继续使用 .Net 4.0 
	并将此版本作为最终发布版归档处理, 下个版本开始仅对 .Net 4.5+ 更新

	1. 修复 悬浮窗数据转换错误 (由 Pack -> BinaryWriter)
	2. 优化 部分 Api 接口的数据处理效率 (由 UnPack -> BinaryReader)
	3. 优化 分离 Native.Csharp.Tool 项目, 使 SDK 更轻量
	4. 优化 调整 Native.Csharp.Tool 项目结构, 每个模块为一个根文件夹. 排除即可在编译时移除功能
	5. 优化 新增 HttpTool (位于 Native.Csharp.Tool.Http)
	6. 新增 SQLite 操作类 (不包含EF, 需要可自行添加), 完全移植自 System.Data.SQLite (.Net 4.0)

> 2019年05月25日 版本: V2.0.5.0525

	1. 修复 HttpWebClient 类在请求 Internet 资源时响应重定向的部分代码错误
	2. 优化 HttpWebClient 类可指定自动合并更新 Cookies 功能
	3. 优化 CqMsg 类代码运行流程, 更符合规范
	4. 优化 CqCode 类, 支持获取当前实例在原文中的位置
	5. 优化 CqCode 类, 支持获取当前实例的原始字符串

> 2019年05月21日 版本: V2.0.4.0521

	1. 修复 编译出错
	2. 修复 兼容组件导致WFP窗体加载会出现资源无法找到

> 2019年05月17日 版本: V2.0.3.0517

	1. 修复 附加进程调试加载符号库出错

> 2019年05月14日 版本: V2.0.2.0514

	1. 修复 Repair 兼容组件不能正确重定向 (但是旧版本要兼容必须先关闭 Costura 的重定向, LoadAtModuleInit="False")
	2. 关闭 Costura 的重定向功能, 且在 SDK 加载时自动初始化

> 2019年05月12日 版本: V2.0.1.0512

	说明: 由于酷Q改动了应用加载机制, 将所有开发中的应用都迁移至 dev 文件夹下, 所以本次更新将针对此优化进行改动

    1. 修复 Native.Csharp.Repair 应用兼容组件运行时间不正确
    2. 新增 SDK 生成前自动检查是否存在 AppID 目录
    3. 新增 SDK 生成前自动清理旧版 app.dll app.json 文件
    4. 新增 SDK 生成后自动复制并重命名程序集为 app.dll 自动复制并重命名json文件为 app.json
    5. 新增 SDK 生成后自动清理原生成目录下的程序集和 json 文件
    6. 转换 Native.Csharp.json 文件为 UTF-8 编码
    7. 修复 Fody 不兼容 Visual Studio 导致编译不通过的问题

> 2019年05月04日 版本: V2.0.0.0504

	说明: 由于酷Q改动了应用机制, 因此升级时请务必保存代码, 进行代码迁移升级!
	注意: 本次升级相对于之前的版本应用间不兼容做出了修改, 但是其机制导致了与旧版不兼容, 请酌情升级!
    
    1. 修复 CqApi.AddFatalError 方法传递错误时可能引发酷Q堆栈错误
    2. 修复 AppDomain.UnhandledException 全局异常捕获失效, 现在支持定位到方法
    3. 优化 AppDemain.UnhandledException 全局异常捕获解析方式
    4. 优化 项目版本号, 统一为项目新增当前版本号以区分
    5. 优化 项目事件模型, 抽象 EventArgsBase 类作为公共抽象类
    6. 优化 CqMsg 类, 完善 CqCodeType 枚举
    7. 优化 CqMsg 类, 更改 CqCode.Content 为字典, 而非键值对集合
    8. 优化 调试机制, 根据 酷Q 应用机制变动而转变为附加调试
    9. 优化 IOC容器的注册方式
    10.新增 Native.Basic.Repair 组件, 组件为原 (@成音S) 大佬的 .Net 兼容组件 (感谢@laomms帮助修改 VB.Net源代码 Github: https://github.com/laomms)
	11.移除 Event_AppMain.Initialize 方法, 保证应用加载效率

> 2019年04月09日 版本: V1.1.3

    1. 修复 CqMsg 类针对 VS2012 的兼容问题
    2. 修复 HttpWebClient 类在增加 Cookies 时, 参数 "{0}" 为空字符串的异常
    3. 新增 HttpWebClient 类属性 "KeepAlive", 允许指定 HttpWebClient 在做请求时是否建立持续型的 Internal 连接

> 2019年04月06日 版本: V1.1.2
	
    1. 优化 Native.Csharp.Sdk 项目的结构, 修改类: CqApi 的命名空间
    2. 新增 消息解析类: CqMsg
    
``` VB
' 使用方法如下, 例如在群消息接受方法中
Public Sub ReceiveGroupMessage(ByVal sender As Object, ByVal e As GroupMessageEventArgs) Implements IEvent_GroupMessage.ReceiveGroupMessage
	Dim parseResult As CqMsg = CqMsg.Parse (e.Msg)		' 使用消息解析
	Dim cqCodes As List(Of CqCode) = parseResult.Contents	' 获取消息中所有的 CQ码
	
	' 此时, 获取到的 cqCodes 中就包含此条消息所有的 CQ码
End Sub
```

> 2019年03月12日 版本: V1.1.1

    1. 新增 Sex 枚举中未知性别, 值为 255
    2. 优化 IOC 容器在获取对象时, 默认拉取所有注入的对象, 简化消息类接口的注入流程.
	
> 2019年03月03日 版本: V1.1.0

	本次更新于响应 "酷Q" 官方 "易语言 SDK" 的迭代更新
	
    1. 新增 CqApi.ReceiveImage (用于获取消息中 "图片" 的绝对路径)
    2. 新增 CqApi.GetSendRecordSupport (用于获取 "是否支持发送语音", 即用于区别 Air 和 Pro 版本之间的区别)
    3. 新增 CqApi.GetSendImageSupport (用于获取 "是否支持发送图片", 即用于区别 Air 和 Pro 版本指间的区别)
    4. 优化 CqApi.ReceiveRecord 方法, 使其获取到的语音路径为绝对路径, 而非相对路径
	
> 2019年02月26日 版本: V1.0.6

    1. 默认注释 Event_GroupMessage 中 ReceiveGroupMessage 方法的部分代码, 防止因为机器人复读群消息而禁言

> 2019年02月24日 版本: V1.0.5

    1. 为 SDK 添加了提交版本号

> 2019年02月20日 版本: V1.0.4

    1. 还原 Event_AppMain.Resolvebackcall 方法的执行, 防止偶尔获取不到注入的类

> 2019年02月20日 版本: V1.0.3

    1. 更新 Native.Basic 项目的部分注释
    2. 新增 Event_AppMain.Initialize 方法, 位于 "Native.Basic.App.Event" 下, 用于当作本项目的初始化方法
    3. 优化 Event_AppMain.Resolvebackcall 方法的执行, 默认将依据接口注入的类型全部实例化并取出分发到事件上 

> 2019年02月16日 版本: V1.0.2

    1. 优化 FodyWeavers.xml 配置, 为其加上注释. 方便开发者使用
    2. 修复 IniValue 中 ToType 方法导致栈溢出的

> 2019年01月27日 版本: V1.0.1

    1. 移除 AnyCPU 配置项, 提升编译稳定性
    2. 提交相关依赖项, 提升编译稳定性

> 2019年01月27日 版本: V1.0.0

    1. 打包上传项目
