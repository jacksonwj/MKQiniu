using System;
using System.IO;
using System.Security.Cryptography;

namespace MKQiniu
{
    internal class Signature
    {
        private string _accessKey;
        private string _secretKey;
        private IQiniuSerializer _serializer;

        internal Signature(string accessKey, string secretKey)
            : this(null, accessKey, secretKey)
        { }

        internal Signature(IQiniuSerializer serializer, string accessKey, string secretKey)
        {
            _accessKey = accessKey;
            _secretKey = secretKey;
            _serializer = serializer;
        }

        internal string Sign(string url)
        {
            return string.Format("{0}:{1}", _accessKey, EncodedSign(url));
        }

        internal string Sign(string url, byte[] bytes)
        {
            var datas = Config.ENCODING.GetBytes(new Uri(url).PathAndQuery);

            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Write(datas, 0, datas.Length);
                memoryStream.WriteByte((byte)'\n');

                if (bytes != null && bytes.Length > 0)
                {
                    memoryStream.Write(bytes, 0, bytes.Length);
                }

                return string.Format("{0}:{1}", _accessKey, Utils.ToBase64String(GetHash(memoryStream.ToArray())));
            }
        }

        internal string Sign(QiniuPolicy policy)
        {
            if (_serializer == null)
            {
                throw new ArgumentNullException("请添加 JSON 序列化组件");
            }

            var base64 = Utils.ToBase64String(_serializer.Serialize(policy));
            return string.Format("{0}:{1}:{2}", _accessKey, EncodedSign(base64), base64);
        }

        private string EncodedSign(string content)
        {
            return Utils.ToBase64String(GetHash(Config.ENCODING.GetBytes(content)));
        }

        private byte[] GetHash(byte[] buffer)
        {
            var mac = new HMACSHA1(Config.ENCODING.GetBytes(_secretKey));
            return mac.ComputeHash(buffer);
        }
    }
}
