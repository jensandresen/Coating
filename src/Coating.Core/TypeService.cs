namespace Coating
{
    public class TypeService : ITypeService
    {
        public string GetTypeNameFrom(object o)
        {
            return o.GetType().Name;
        }
    }
}