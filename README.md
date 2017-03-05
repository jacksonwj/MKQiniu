# MKQiniu
简单重写七牛方法，实现扩展 JSON 自定义组件

### 重写了JSON序列化接口
部分方法方法参考了 [七牛官方 SDK 7.2.14](https://github.com/qiniu/csharp-sdk/releases/tag/v7.2.14)

### 实现了以下几个内容：
- 获取上传/下载/管理 Token
- 实现了文件生命周期管理 UpdateLifecycle
- 自动获取 Zone（含手动设置Zone）
- 目前基于 [Jil](https://github.com/kevin-montrose/Jil) 和 [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) 对 JSON 序列化进行扩展
