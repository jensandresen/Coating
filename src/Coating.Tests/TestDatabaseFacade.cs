using System.Data;
using Moq;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestDatabaseFacade
    {
        [Test]
        public void returns_expected_connection()
        {
            var expected = new Mock<IDbConnection>().Object;
            var sut = new DatabaseFacadeBuilder()
                .WithConnection(expected)
                .Build();

            Assert.AreEqual(expected, sut.Connection);
        }

        [Test]
        public void creates_expected_command_for_insert()
        {
            var mockCommandFactory = new Mock<ICommandFactory>();

            var sut = new DatabaseFacadeBuilder()
                .WithCommandFactory(mockCommandFactory.Object)
                .Build();

            var stubDocument = new Document
                {
                    Id = "1",
                    Data = "foo",
                    Type = "bar"
                };

            sut.Insert(stubDocument);

            mockCommandFactory.Verify(x => x.CreateInsertCommandFor(stubDocument.Id, stubDocument.Data, stubDocument.Type));
        }

        [Test]
        public void executes_insert_command()
        {
            var mock = new Mock<ICommandExecutor>();
            
            var dummyDocument = new Document();
            var dummyInsertCommand = new SqlCommandBuilder().Build();

            var sut = new DatabaseFacadeBuilder()
                .WithCommandFactory(new StubCommandFactory(insert: dummyInsertCommand))
                .WithExecutor(mock.Object)
                .Build();

            sut.Insert(dummyDocument);

            mock.Verify(x => x.Execute(dummyInsertCommand));
        }
    }

    public class StubCommandFactory : ICommandFactory
    {
        private readonly SqlCommand _insert;
        private readonly SqlCommand _update;
        private readonly SqlCommand _delete;
        private readonly SqlCommand _selectById;
        private readonly SqlCommand _selectByType;

        public StubCommandFactory(SqlCommand insert = null, 
            SqlCommand update = null,
            SqlCommand delete = null,
            SqlCommand selectById = null,
            SqlCommand selectByType = null)
        {
            _insert = insert;
            _update = update;
            _delete = delete;
            _selectById = selectById;
            _selectByType = selectByType;
        }

        public SqlCommand CreateInsertCommandFor(string id, string data, string typeName)
        {
            return _insert;
        }

        public SqlCommand CreateUpdateCommandFor(string id, string theNewData, string typeName)
        {
            return _update;
        }

        public SqlCommand CreateDeleteCommandFor(string id)
        {
            return _delete;
        }

        public SqlCommand CreateSelectByIdCommandFor(string id)
        {
            return _selectById;
        }

        public SqlCommand CreateSelectByTypeCommandFor(string typeName)
        {
            return _selectByType;
        }
    }

    public class SqlCommandBuilder
    {
        public SqlCommand Build()
        {
            return new SqlCommand();
        }
    }

    public interface ICommandExecutor
    {
        void Execute(SqlCommand sqlCommand);
    }

    public class Document
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public string Type { get; set; }
    }

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

        public void Insert(Document document)
        {
            var cmd = _commandFactory.CreateInsertCommandFor(document.Id, document.Data, document.Type);
            _commandExecutor.Execute(cmd);
        }
    }

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