using Jil;

namespace MKQiniu.Jil
{
    public class JilSerializer : IQiniuSerializer
    {
        public JilSerializer(Options options = null)
        {
            if (options == null)
            {
                options = new Options(excludeNulls: true,
                                      dateFormat: DateTimeFormat.ISO8601,
                                      includeInherited: true,
                                      serializationNameFormat: SerializationNameFormat.CamelCase);
            }

            JSON.SetDefaultOptions(options);
        }

        public byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            return Config.ENCODING.GetBytes(JSON.Serialize(obj));
        }

        public T Deserialize<T>(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return default(T);
            }

            return JSON.Deserialize<T>(content);
        }
    }
}
