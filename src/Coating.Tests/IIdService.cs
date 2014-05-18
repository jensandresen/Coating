namespace Coating.Tests
{
    public interface IIdService
    {
        string GetIdFrom<T>(T o) where T : class;
    }
}