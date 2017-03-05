using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MKQiniu
{
    internal class HttpManager
    {
        private HttpClient _httpClient;

        internal HttpManager(bool allowAutoRedirect = false)
        {
            _httpClient = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = allowAutoRedirect });
        }

        ~HttpManager()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }

        internal HttpResult Get(string url, string token, bool isBinaryMode = false)
        {
            var result = new HttpResult();

            try
            {
                var reqMessage = new HttpRequestMessage(HttpMethod.Get, url);
                reqMessage.Headers.Add("User-Agent", Config.USER_AGENT);

                if (!string.IsNullOrWhiteSpace(token))
                {
                    reqMessage.Headers.Add("Authorization", token);
                }

                var clientResult = _httpClient.SendAsync(reqMessage).Result;
                result.Code = (int)clientResult.StatusCode;
                result.RefCode = result.Code;

                if (isBinaryMode)
                {
                    result.Data = clientResult.Content.ReadAsByteArrayAsync().Result;
                }
                else
                {
                    result.Text = clientResult.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception exception)
            {
                var builder = new StringBuilder();
                builder.AppendFormat("[{0:yyyy-MM-dd HH:mm:ss.ffff}] [{1}] [HTTP-GET] Error:", DateTime.Now, Config.USER_AGENT);
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

        internal HttpResult Post(string url, string token, bool isBinaryMode = false)
        {
            var result = new HttpResult();

            try
            {
                var reqMessage = new HttpRequestMessage(HttpMethod.Post, url);
                reqMessage.Headers.Add("User-Agent", Config.USER_AGENT);

                if (!string.IsNullOrWhiteSpace(token))
                {
                    reqMessage.Headers.Add("Authorization", token);
                }

                var clientResult = _httpClient.SendAsync(reqMessage).Result;
                result.Code = (int)clientResult.StatusCode;
                result.RefCode = result.Code;

                GetHeaders(ref result, clientResult);

                if (isBinaryMode)
                {
                    result.Data = clientResult.Content.ReadAsByteArrayAsync().Result;
                }
                else
                {
                    result.Text = clientResult.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception exception)
            {
                var builder = new StringBuilder();
                builder.AppendFormat("[{0:yyyy-MM-dd HH:mm:ss.ffff}] [{1}] [HTTP-POST] Error:", DateTime.Now, Config.USER_AGENT);
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

        private void GetHeaders(ref HttpResult result, HttpResponseMessage resMessage)
        {
            if (resMessage == null)
            {
                return;
            }

            if ((resMessage.Content.Headers != null || resMessage.Headers != null) && result.RefInfo == null)
            {
                result.RefInfo = new Dictionary<string, string>();
            }

            var key = string.Empty;
            var listVal = new List<string>();

            foreach (var header in resMessage.Content.Headers)
            {
                listVal.Clear();
                key = header.Key;

                foreach (var per in header.Value)
                {
                    listVal.Add(per);
                }

                if (listVal.Count < 1)
                {
                    continue;
                }

                result.RefInfo.Add(key, string.Join("; ", listVal));
            }

            foreach (var header in resMessage.Headers)
            {
                listVal.Clear();
                key = header.Key;

                foreach (var per in header.Value)
                {
                    listVal.Add(per);
                }

                if (listVal.Count < 1)
                {
                    continue;
                }

                result.RefInfo.Add(key, string.Join("; ", listVal));
            }
        }
    }
}
