namespace Coating.Core
{
    public interface IIdService
    {
        string GetIdFrom<T>(T o) where T : class;
    }
}