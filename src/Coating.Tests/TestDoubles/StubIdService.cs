namespace Coating.Tests.TestDoubles
{
    public class StubIdService : IIdService
    {
        private readonly string _result;

        public StubIdService(string result)
        {
            _result = result;
        }

        public string GetIdFrom<T>(T o) where T : class
        {
            return _result;
        }
    }
}