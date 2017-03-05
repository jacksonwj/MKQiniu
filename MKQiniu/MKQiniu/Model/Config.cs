using System;
using System.Text;

namespace MKQiniu
{
    public class Config
    {
        /// <summary>
        /// 编码格式
        /// <example>Encoding.UTF8</example>
        /// </summary>
        public static readonly Encoding ENCODING = Encoding.UTF8;

        internal const string ALIAS = "QiniuCSharpSDK";

        internal const string VERSION = "1.0.0";

        internal const string RTFX = "NET46";

        /// <summary>
        /// 用户平台
        /// </summary>
        internal static string USER_AGENT
        {
            get
            {
                var os = Environment.OSVersion.Platform + "; " + Environment.OSVersion.Version;
                return string.Format("{0}/{1} ({2}; {3})", ALIAS, VERSION, RTFX, os);
            }
        }

        /// <summary>
        /// 时间戳
        /// <example>1970/1/1 00:00:00</example>
        /// </summary>
        internal static TimeSpan TIMESPAN
        {
            get { return DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)); }
        }

        /// <summary>
        /// 区域配置
        /// <example>默认：华东</example>
        /// </summary>
        internal static Zone ZONE = Zone.GetZone(ZoneID.Default);
    }
}
