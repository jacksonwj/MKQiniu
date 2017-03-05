using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MKQiniu.Newtonsoft
{
    public class NewtonsoftSerializer : IQiniuSerializer
    {
        private readonly JsonSerializerSettings _settings;

        public NewtonsoftSerializer(JsonSerializerSettings settings = null)
        {
            _settings = settings ?? new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    NullValueHandling = NullValueHandling.Ignore
                };
        }

        public byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            return Config.ENCODING.GetBytes(JsonConvert.SerializeObject(obj, obj.GetType(), _settings));
        }

        public T Deserialize<T>(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(content, _settings);
        }
    }
}
