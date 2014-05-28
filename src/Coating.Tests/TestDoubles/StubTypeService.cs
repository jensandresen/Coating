namespace Coating.Tests.TestDoubles
{
    public class StubTypeService : ITypeService
    {
        private readonly string _result;

        public StubTypeService(string result)
        {
            _result = result;
        }

        public string GetTypeNameFrom(object instance)
        {
            return _result;
        }

        public string GetTypeNameFrom<T>()
        {
            return _result;
        }
    }
}