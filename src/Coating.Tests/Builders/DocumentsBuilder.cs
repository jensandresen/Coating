using System.Data;
using Moq;

namespace Coating.Tests.Builders
{
    public class DocumentsBuilder
    {
        private IDbConnection _dbConnection;
        private ICommandExecutor _commandExecutor;
        private ICommandFactory _commandFactory;

        public DocumentsBuilder()
        {
            _dbConnection = new Mock<IDbConnection>().Object;
            _commandExecutor = new Mock<ICommandExecutor>().Object;
            _commandFactory = new Mock<ICommandFactory>().Object;
        }

        public DocumentsBuilder WithDbConnection(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            return this;
        }

        public DocumentsBuilder WithCommandExecutor(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
            return this;
        }

        public DocumentsBuilder WithCommandFactory(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            return this;
        }

        public Documents Build()
        {
            return new Documents(_dbConnection, _commandExecutor, _commandFactory);
        }
    }
}