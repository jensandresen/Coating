namespace Coating.Tests
{
    public class TypeService
    {
        public string GetTypeNameFrom(object o)
        {
            return o.GetType().Name;
        }
    }
}