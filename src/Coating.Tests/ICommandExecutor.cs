namespace Coating.Tests
{
    public interface ICommandExecutor
    {
        void ExecuteWriteCommand(SqlCommand sqlCommand);
    }
}