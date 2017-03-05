using System;

namespace MKQiniu
{
    internal class Utils
    {
        internal static string ToBase64String(string bucket, string key)
        {
            return ToBase64String(bucket + ":" + key);
        }

        internal static string ToBase64String(string content)
        {
            return ToBase64String(Config.ENCODING.GetBytes(content));
        }

        internal static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_');
        }
    }
}
