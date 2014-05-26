namespace Coating.Tests.TestDoubles
{
    public class StubTypeService : ITypeService
    {
        private readonly string _result;

        public StubTypeService(string result)
        {
            _result = result;
        }

        public string GetTypeNameFrom(object o)
        {
            return _result;
        }
    }
}