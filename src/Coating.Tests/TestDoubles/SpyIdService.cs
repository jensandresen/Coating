namespace Coating.Tests.TestDoubles
{
    public class SpyIdService : IIdService
    {
        public bool wasCalled = false;

        public string GetIdFrom<T>(T o) where T : class
        {
            wasCalled = true;
            return "";
        }
    }
}