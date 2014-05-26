namespace Coating
{
    public interface ICommandExecutor
    {
        void ExecuteWriteCommand(SqlCommand sqlCommand);
    }
}