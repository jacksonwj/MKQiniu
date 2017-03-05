using System.Collections.Generic;
using System.Text;

namespace MKQiniu
{
    public class HttpResult
    {
        /// <summary>
        /// 状态码 (200表示OK)
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息或错误文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 消息或错误(二进制格式)
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 参考代码(用户自定义)
        /// </summary>
        public int RefCode { get; set; }

        /// <summary>
        /// 附加信息(用户自定义,如Exception内容)
        /// </summary>
        public string RefText { get; set; }

        /// <summary>
        /// 参考信息(从返回消息WebResponse的头部获取)
        /// </summary>
        public Dictionary<string, string> RefInfo { get; set; }

        /// <summary>
        /// 初始化(所有成员默认值，需要后续赋值)
        /// </summary>
        public HttpResult()
        {
            Code = (int)HttpCode.USER_UNDEF;
            RefCode = Code;
        }

        /// <summary>
        /// 转换为易读或便于打印的字符串格式
        /// </summary>
        /// <returns>便于打印和阅读的字符串</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("code:{0}", Code);
            builder.AppendLine();

            if (!string.IsNullOrWhiteSpace(Text))
            {
                builder.AppendLine("text:");
                builder.AppendLine(Text);
            }

            if (Data != null)
            {
                builder.AppendLine("data:");
                var n = 1024;

                if (Data.Length <= n)
                {
                    builder.AppendLine(Config.ENCODING.GetString(Data));
                }
                else
                {
                    builder.AppendLine(Config.ENCODING.GetString(Data, 0, n));
                    builder.AppendFormat("<--- TOO-LARGE-TO-DISPLAY --- TOTAL {0} BYTES --->", Data.Length);
                    builder.AppendLine();
                }
            }

            builder.AppendLine();
            builder.AppendFormat("ref-code:{0}", RefCode);
            builder.AppendLine();

            if (!string.IsNullOrWhiteSpace(RefText))
            {
                builder.AppendLine("ref-text:");
                builder.AppendLine(RefText);
            }

            if (RefInfo != null)
            {
                builder.AppendLine("ref-info:");

                foreach (var info in RefInfo)
                {
                    builder.AppendLine(string.Format("{0}:{1}", info.Key, info.Value));
                }
            }

            builder.AppendLine();
            return builder.ToString();
        }
    }
}
