namespace Coating.Tests.TestDoubles
{
    public class StubSerializationService :  ISerializationService
    {
        private readonly string _serializationResult;
        private readonly object _deserializationResult;

        public StubSerializationService(string serializationResult = null, object deserializationResult = null)
        {
            _serializationResult = serializationResult;
            _deserializationResult = deserializationResult;
        }

        public string Serialize(object o)
        {
            return _serializationResult;
        }

        public T Deserialize<T>(string jsonData) where T : class
        {
            return (T) _deserializationResult;
        }
    }
}