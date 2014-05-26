using System.Data;
using Moq;

namespace Coating.Tests.Builders
{
    public class DatabaseFacadeBuilder
    {
        private IDbConnection _connection;
        private ICommandFactory _commandFactory;
        private ICommandExecutor _commandExecutor;

        public DatabaseFacadeBuilder()
        {
            _connection = new Mock<IDbConnection>().Object;
            _commandFactory = new Mock<ICommandFactory>().Object;
            _commandExecutor = new Mock<ICommandExecutor>().Object;
        }

        public DatabaseFacadeBuilder WithConnection(IDbConnection connection)
        {
            _connection = connection;
            return this;
        }

        public DatabaseFacadeBuilder WithCommandFactory(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            return this;
        }

        public DatabaseFacadeBuilder WithExecutor(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
            return this;
        }

        public DatabaseFacade Build()
        {
            return new DatabaseFacade(_connection, _commandFactory, _commandExecutor);
        }
    }
}