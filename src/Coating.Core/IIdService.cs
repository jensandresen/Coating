namespace Coating
{
    public interface IIdService
    {
        string GetIdFrom<T>(T o) where T : class;
    }
}