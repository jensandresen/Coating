using System.Data;
using Moq;

namespace Coating.Tests
{
    public class DefaultCommandExecutorBuilder
    {
        private IDbConnection _dbConnection;
        private ICommandMapper _mapper;

        public DefaultCommandExecutorBuilder()
        {
            _dbConnection = new Mock<IDbConnection>().Object;
            _mapper = new Mock<ICommandMapper>().Object;
        }

        public DefaultCommandExecutorBuilder WithDbConnection(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            return this;
        }

        public DefaultCommandExecutorBuilder WithMapper(ICommandMapper mapper)
        {
            _mapper = mapper;
            return this;
        }

        public DefaultCommandExecutor Build()
        {
            return new DefaultCommandExecutor(_dbConnection, _mapper);
        }
    }
}