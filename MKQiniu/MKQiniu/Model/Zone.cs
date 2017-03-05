using System;
using System.Collections.Generic;
using System.Text;

namespace MKQiniu
{
    internal class ZoneInfo
    {
        public int TTL { get; set; }

        public OBulk HTTP { get; set; }

        public OBulk HTTPS { get; set; }
    }

    /// <summary>
    /// 包含uploadHost和iovip等
    /// </summary>
    internal class OBulk
    {
        public string[] UP { get; set; }
        public string[] IO { get; set; }
    }

    public enum ZoneID
    {
        /// <summary>
        /// 华东
        /// </summary>
        CN_East,

        /// <summary>
        /// 华北
        /// </summary>
        CN_North,

        /// <summary>
        /// 华南
        /// </summary>
        CN_South,

        /// <summary>
        /// 北美
        /// </summary>
        US_North,

        /// <summary>
        /// 默认-华东
        /// </summary>
        Default = CN_East,

        /// <summary>
        /// 未定义,比如QueryZone查询出错时设置为此值
        /// </summary>
        Invalid = 999
    };

    internal class Zone
    {
        private static Dictionary<string, ZoneID> _ZONE_DICT = new Dictionary<string, ZoneID>()
            {
                {"http://up.qiniu.com",     ZoneID.CN_East },
                {"http://up-z1.qiniu.com",  ZoneID.CN_North},
                {"http://up-z2.qiniu.com",  ZoneID.CN_South},
                {"http://up-na0.qiniu.com", ZoneID.US_North}
            };

        /// <summary>
        /// 资源管理
        /// </summary>
        public string RsHost { set; get; }

        /// <summary>
        /// 源列表
        /// </summary>
        public string RsfHost { set; get; }

        /// <summary>
        /// 数据处理
        /// </summary>
        public string ApiHost { set; get; }

        /// <summary>
        /// 镜像刷新、资源抓取
        /// </summary>
        public string IovipHost { set; get; }

        /// <summary>
        /// 资源上传
        /// </summary>
        public string UpHost { set; get; }

        /// <summary>
        /// CDN加速
        /// </summary>
        public string UploadHost { set; get; }

        /// <summary>
        /// 根据ZoneID取得对应Zone设置
        /// </summary>
        /// <param name="zoneId">区域编号</param>
        /// <param name="useHTTPS">是否使用HTTPS</param>
        /// <returns></returns>
        internal static Zone GetZone(ZoneID zoneId, bool useHTTPS = false)
        {
            switch (zoneId)
            {
                case ZoneID.CN_North:
                    return ZONE_CN_North(useHTTPS);
                case ZoneID.CN_South:
                    return ZONE_CN_South(useHTTPS);
                case ZoneID.US_North:
                    return ZONE_US_North(useHTTPS);
                case ZoneID.CN_East:
                default:
                    return ZONE_CN_East(useHTTPS);
            }
        }

        /// <summary>
        /// 华东
        /// xx-(NULL)
        /// </summary>
        /// <param name="useHTTPS">是否使用HTTPS</param>
        internal static Zone ZONE_CN_East(bool useHTTPS)
        {
            var httpPrefix = "http" + (useHTTPS ? "s" : string.Empty) + "://";
            var domainSuffix = useHTTPS ? "qbox.me" : "qiniu.com";

            return new Zone()
                {
                    RsHost = httpPrefix + "rs.qbox.me",
                    RsfHost = httpPrefix + "rsf.qbox.me",
                    ApiHost = httpPrefix + "api.qiniu.com",
                    IovipHost = httpPrefix + "iovip.qbox.me",
                    UpHost = httpPrefix + "up." + domainSuffix,
                    UploadHost = httpPrefix + "upload." + domainSuffix
                };
        }

        /// <summary>
        /// 华北
        /// xx-z1
        /// </summary>
        /// <param name="useHTTPS">是否使用HTTPS</param>
        internal static Zone ZONE_CN_North(bool useHTTPS)
        {
            var httpPrefix = "http" + (useHTTPS ? "s" : string.Empty) + "://";
            var domainSuffix = useHTTPS ? "qbox.me" : "qiniu.com";

            return new Zone()
                {
                    RsHost = httpPrefix + "rs-z1.qbox.me",
                    RsfHost = httpPrefix + "rsf-z1.qbox.me",
                    ApiHost = httpPrefix + "api-z1.qiniu.com",
                    IovipHost = httpPrefix + "iovip-z1.qbox.me",
                    UpHost = httpPrefix + "up-z1." + domainSuffix,
                    UploadHost = httpPrefix + "upload-z1." + domainSuffix
                };
        }

        /// <summary>
        /// 华南
        /// xx-z2
        /// </summary>
        /// <param name="useHTTPS">是否使用HTTPS</param>
        internal static Zone ZONE_CN_South(bool useHTTPS)
        {
            var httpPrefix = "http" + (useHTTPS ? "s" : string.Empty) + "://";
            var domainSuffix = useHTTPS ? "qbox.me" : "qiniu.com";

            return new Zone()
                {
                    RsHost = httpPrefix + "rs-z2.qbox.me",
                    RsfHost = httpPrefix + "rsf-z2.qbox.me",
                    ApiHost = httpPrefix + "api-z2.qiniu.com",
                    IovipHost = httpPrefix + "iovip-z2.qbox.me",
                    UpHost = httpPrefix + "up-z2." + domainSuffix,
                    UploadHost = httpPrefix + "upload-z2." + domainSuffix
                };
        }

        /// <summary>
        /// 北美
        /// xx-na0
        /// </summary>
        /// <param name="useHTTPS">是否使用HTTPS</param>
        /// <returns></returns>
        internal static Zone ZONE_US_North(bool useHTTPS)
        {
            var httpPrefix = "http" + (useHTTPS ? "s" : string.Empty) + "://";
            var domainSuffix = useHTTPS ? "qbox.me" : "qiniu.com";

            return new Zone()
                {
                    RsHost = httpPrefix + "rs-na0.qbox.me",
                    RsfHost = httpPrefix + "rsf-na0.qbox.me",
                    ApiHost = httpPrefix + "api-na0.qiniu.com",
                    IovipHost = httpPrefix + "iovip-na0.qbox.me",
                    UpHost = httpPrefix + "up-na0." + domainSuffix,
                    UploadHost = httpPrefix + "upload-na0." + domainSuffix
                };
        }

        internal static ZoneID GetZone(IQiniuSerializer serializer, string accessKey, string bucket)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("请添加 JSON 序列化组件");
            }

            var zoneID = ZoneID.Invalid;

            try
            {
                var url = "https://uc.qbox.me/v1/query?ak=" + accessKey + "&bucket=" + bucket;
                var result = new HttpManager().Get(url, null);
                if (result.Code != (int)HttpCode.OK)
                {
                    throw new Exception("code: " + result.Code + ", text: " + result.Text + ", ref-text:" + result.RefText);
                }

                var info = serializer.Deserialize<ZoneInfo>(result.Text);
                if (info == null)
                {
                    throw new Exception("JSON Deserialize failed: " + result.Text);
                }

                zoneID = _ZONE_DICT[info.HTTP.UP[0]];
            }
            catch (Exception exception)
            {
                var builder = new StringBuilder();
                builder.AppendFormat("[{0:yyyy-MM-dd HH:mm:ss.ffff}] QueryZone Error:", DateTime.Now);
                var tempEx = exception;

                while (tempEx != null)
                {
                    builder.AppendFormat(" {0}", tempEx.Message);
                    tempEx = tempEx.InnerException;
                }

                builder.AppendLine();
                throw new Exception(builder.ToString());
            }

            return zoneID;
        }
    }
}
