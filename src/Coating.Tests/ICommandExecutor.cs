namespace Coating.Tests
{
    public interface ICommandExecutor
    {
        void Execute(SqlCommand sqlCommand);
    }
}