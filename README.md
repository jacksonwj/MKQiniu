# MKQiniu C# SDK
简单重写七牛的方法，实现了自定义 JSON 解析器

### 开发环境
- Visual Studio 2013 Update 5
- .NET Framework 4.6.2
- Jil 2.15.0
- Sigil 4.7.0
- Newtonsoft.Json 9.0.1

### 实现 JSON 解析器接口（IQiniuSerializer）
其余部分方法方法参考了 [七牛官方 C# SDK 7.2.14](https://github.com/qiniu/csharp-sdk/releases/tag/v7.2.14)

### 实现了以下几个内容：
- 去掉了 PutPolicy 中的 SetExpires、ToJsonString 两个方法（利用自定义解析器对 PutPolicy 进行 JSON 序列化）
- 优化了部分代码
- 获取上传/下载/管理 Token
- 实现了单个文件生命周期管理 UpdateLifecycle
- 自动获取 Zone（含手动设置 Zone）
- 目前实现了基于 [Jil](https://github.com/kevin-montrose/Jil) 和 [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) 对 JSON 的解析

### 测试情况（详见内置单元测试项目）
- 正常获取到了上传/下载 Token，并成功与服务器完成了相应的操作
- 正常获取到了管理 Token，并成功完成了单个文件的生命周期管理
- 正常设置了 Zone，也成功自动获取到了 ZoneID（利用 AutoZone）
- 成功用 Jil 或 Json.NET 对内部实体进行 JSON 序列化/反序列化
