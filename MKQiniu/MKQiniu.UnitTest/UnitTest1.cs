using Microsoft.VisualStudio.TestTools.UnitTesting;

using MKQiniu.Jil;
using MKQiniu.Newtonsoft;

namespace MKQiniu.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private const string _AK = "access_key";
        private const string _SK = "secret_key";
        private const string _BUCKET = "bucket";
        private const int _EXPIRE = 1;
        private const string _END_USER = "123";

        [TestMethod]
        public void TestPolicyToJsonString()
        {
            //var qiniu = new Qiniu(_AK, _SK);

            //qiniu.PutPolicy.Scope = _BUCKET;
            //qiniu.PutPolicy.Deadline = _EXPIRE;

            //qiniu.PutPolicy.EndUser = _END_USER;

            //var result = qiniu.GetUploadToken();
        }

        [TestMethod]
        public void TestPolicyToJsonStringByJil()
        {
            var qiniu = new Qiniu(_AK, _SK);

            // 采用 Jil 序列化 JSON
            qiniu.Serializer = new JilSerializer();

            qiniu.PutPolicy.Scope = _BUCKET;
            qiniu.PutPolicy.Deadline = _EXPIRE;

            qiniu.PutPolicy.EndUser = _END_USER;

            var result = qiniu.GetUploadToken();
        }

        [TestMethod]
        public void TestPolicyToJsonStringByNewtonsoft()
        {
            var qiniu = new Qiniu(_AK, _SK);

            // 采用 Newtonsoft 序列化 JSON
            qiniu.Serializer = new NewtonsoftSerializer();

            qiniu.PutPolicy.Scope = _BUCKET;
            qiniu.PutPolicy.Deadline = _EXPIRE;

            qiniu.PutPolicy.EndUser = _END_USER;

            var result = qiniu.GetUploadToken();
        }

        [TestMethod]
        public void TestUploadLifecycle()
        {
            var qiniu = new Qiniu(_AK, _SK);
            qiniu.PutPolicy.Scope = _BUCKET;

            var result = qiniu.UpdateLifecycle("123", 3);
        }

        [TestMethod]
        public void TestAutoZone()
        {
            var qiniu = new Qiniu(_AK, _SK, _BUCKET);

            //qiniu.Serializer = new NewtonsoftSerializer();

            qiniu.Serializer = new JilSerializer();

            qiniu.AutoZone(true);
        }
    }
}
