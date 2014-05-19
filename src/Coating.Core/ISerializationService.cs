namespace Coating
{
    public interface ISerializationService
    {
        string Serialize(object o);
        T Deserialize<T>(string jsonData) where T : class;
    }
}