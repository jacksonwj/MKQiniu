using System;

namespace MKQiniu
{
    public class Qiniu
    {
        private string _accessKey = string.Empty;
        private string _secretKey = string.Empty;

        /// <summary>
        /// 原始链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 请求的主体内容
        /// </summary>
        public byte[] ManageBody { get; set; }

        /// <summary>
        /// 上传策略
        /// </summary>
        public QiniuPolicy PutPolicy { get; set; }

        /// <summary>
        /// 自定义 JSON 序列化组件
        /// </summary>
        public IQiniuSerializer Serializer = null;

        public Qiniu(string accessKey, string secretKey)
            : this(accessKey, secretKey, null)
        { }

        public Qiniu(string accessKey, string secretKey, string bucket)
        {
            _accessKey = accessKey;
            _secretKey = secretKey;
            PutPolicy = new QiniuPolicy() { Scope = bucket };
        }

        public void SetZone(ZoneID zoneId, bool useHTTPS = false)
        {
            if (zoneId == ZoneID.Invalid)
            {
                return;
            }

            Config.ZONE = Zone.GetZone(zoneId, useHTTPS);
        }

        public void AutoZone(bool useHTTPS = false)
        {
            var zoneID = Zone.GetZone(Serializer, _accessKey, PutPolicy.Scope);
            if (zoneID == ZoneID.Invalid)
            {
                return;
            }

            SetZone(zoneID, useHTTPS);
        }

        public string GetUploadToken()
        {
            return new Signature(Serializer, _accessKey, _secretKey).Sign(PutPolicy);
        }

        public string GetDownloadToken()
        {
            if (string.IsNullOrWhiteSpace(Url))
            {
                return string.Empty;
            }

            return new Signature(_accessKey, _secretKey).Sign(Url);
        }

        public string GetManageToken()
        {
            if (string.IsNullOrWhiteSpace(Url))
            {
                return string.Empty;
            }

            return "QBox " + new Signature(_accessKey, _secretKey).Sign(Url, ManageBody);
        }

        public HttpResult UpdateLifecycle(string key, int deleteAfterDays)
        {
            var bucketManager = new BucketManager(PutPolicy.Scope);
            Url = Config.ZONE.RsHost + bucketManager.GetLifecycleOP(key, deleteAfterDays);
            bucketManager.ManageToken = GetManageToken();
            return bucketManager.UpdateLifecycle(Url, key, deleteAfterDays);
        }
    }
}
