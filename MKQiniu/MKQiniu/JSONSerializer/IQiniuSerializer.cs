namespace MKQiniu
{
    public interface IQiniuSerializer
    {
        byte[] Serialize(object obj);

        T Deserialize<T>(string content);
    }
}
