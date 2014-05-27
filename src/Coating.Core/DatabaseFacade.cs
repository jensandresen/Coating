namespace Coating
{
    public class DatabaseFacade : IDatabaseFacade
    {
        private readonly ICommandFactory _commandFactory;
        private readonly ICommandExecutor _commandExecutor;

        public DatabaseFacade(ICommandFactory commandFactory, ICommandExecutor commandExecutor)
        {
            _commandFactory = commandFactory;
            _commandExecutor = commandExecutor;
        }

        public void Insert(DataDocument document)
        {
            var cmd = _commandFactory.CreateInsertCommandFor(document.Id, document.Data, document.Type);
            _commandExecutor.ExecuteWriteCommand(cmd);
        }
    }
}