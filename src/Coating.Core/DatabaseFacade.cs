using System.Data;

namespace Coating
{
    public class DatabaseFacade
    {
        private readonly IDbConnection _connection;
        private readonly ICommandFactory _commandFactory;
        private readonly ICommandExecutor _commandExecutor;

        public DatabaseFacade(IDbConnection connection, ICommandFactory commandFactory, ICommandExecutor commandExecutor)
        {
            _connection = connection;
            _commandFactory = commandFactory;
            _commandExecutor = commandExecutor;
        }

        public IDbConnection Connection
        {
            get { return _connection; }
        }

        public void Insert(DataDocument document)
        {
            var cmd = _commandFactory.CreateInsertCommandFor(document.Id, document.Data, document.Type);
            _commandExecutor.ExecuteWriteCommand(cmd);
        }
    }
}