namespace Coating.Tests.TestDoubles
{
    public class StubSerializationService :  ISerializationService
    {
        private readonly string _result;

        public StubSerializationService(string result)
        {
            _result = result;
        }

        public string Serialize(object o)
        {
            return _result;
        }

        public T Deserialize<T>(string jsonData) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}