using System.Data;
using Moq;

namespace Coating.Tests.Builders
{
    public class TransactionalCommandExecutorBuilder
    {
        private IDbConnection _connection;
        private ICommandMapper _mapper;

        public TransactionalCommandExecutorBuilder()
        {
            _connection = new Mock<IDbConnection>().Object;
            _mapper = new Mock<ICommandMapper>().Object;
        }

        public TransactionalCommandExecutorBuilder WithConnection(IDbConnection connection)
        {
            _connection = connection;
            return this;
        }

        public TransactionalCommandExecutorBuilder WithMapper(ICommandMapper mapper)
        {
            _mapper = mapper;
            return this;
        }

        public TransactionalCommandExecutor Build()
        {
            return new TransactionalCommandExecutor(_connection, _mapper);
        }
    }
}