using System;
using System.Text;

namespace MKQiniu
{
    internal class BucketManager
    {
        private string _bucket;
        private HttpManager _httpManager;

        internal string Url { get; set; }

        internal string ManageToken { get; set; }

        internal BucketManager(string bucket)
        {
            _bucket = bucket;
            _httpManager = new HttpManager();
        }

        internal string GetLifecycleOP(string key, int deleteAfterDays)
        {
            return string.Format("/deleteAfterDays/{0}/{1}", Utils.ToBase64String(_bucket, key), deleteAfterDays);
        }

        internal HttpResult UpdateLifecycle(string url, string key, int deleteAfterDays)
        {
            var result = new HttpResult();

            try
            {
                result = _httpManager.Post(url, ManageToken);
            }
            catch (Exception exception)
            {
                var builder = new StringBuilder();
                builder.AppendFormat("[{0:yyyy-MM-dd HH:mm:ss.ffff}] [deleteAfterDays] Error:", DateTime.Now);
                var tempEx = exception;

                while (tempEx != null)
                {
                    builder.AppendFormat(" {0}", tempEx.Message);
                    tempEx = tempEx.InnerException;
                }

                builder.AppendLine();
                result.RefCode = (int)HttpCode.USER_EXCEPTION;
                result.RefText += builder.ToString();
            }

            return result;
        }
    }
}
