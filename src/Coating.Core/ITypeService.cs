namespace Coating
{
    public interface ITypeService
    {
        string GetTypeNameFrom(object instance);
        string GetTypeNameFrom<T>();
    }
}