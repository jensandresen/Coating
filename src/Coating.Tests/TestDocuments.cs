using System.Data;
using System.Data.SqlClient;
using Moq;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestDocuments
    {
        [Test]
        public void dbconnection_returns_expected()
        {
            var expectedConnection = new Mock<IDbConnection>().Object;

            var sut = new DocumentsBuilder()
                .WithDbConnection(expectedConnection)
                .Build();

            Assert.AreSame(expectedConnection, sut.DbConnection);
        }

        [Test]
        public void command_executor_returns_expected()
        {
            var expectedExecutor = new Mock<ISqlCommandExecutor>().Object;

            var sut = new DocumentsBuilder()
                .WithSqlCommandExecutor(expectedExecutor)
                .Build();

            Assert.AreSame(expectedExecutor, sut.SqlCommandExecutor);
        }

//        [Test, Ignore]
//        public void store_executes_expected_insert_command()
//        {
//            var mockExecutor = new Mock<ISqlCommandExecutor>();
//            
//            var dummyInsertCommand = new SqlCommand();
//            var dummyDbConnection = new Mock<IDbConnection>().Object;
//
//            var sut = new DocumentsBuilder()
//                .WithDbConnection(dummyDbConnection)
//                .WithSqlCommandExecutor(mockExecutor.Object)
//                .WithCommandFactory(new StubCommandFactory(dummyInsertCommand))
//                .Build();
//
//            var dummyDocument = new object();
//            sut.Save(dummyDocument);
//
//            mockExecutor.Verify(x => x.ExecuteNonQuery(dummyDbConnection, dummyInsertCommand));
//        }

        private class Foo
        {
            public string Id { get; set; }
        }
    }

    public interface ISqlCommandExecutor
    {
        void ExecuteNonQuery(IDbConnection dbConnection, SqlCommand sqlCommand);
    }

    public class DocumentsBuilder
    {
        private IDbConnection _dbConnection;
        private ISqlCommandExecutor _sqlCommandExecutor;
        private ICommandFactory _commandFactory;

        public DocumentsBuilder()
        {
            _dbConnection = new Mock<IDbConnection>().Object;
            _sqlCommandExecutor = new Mock<ISqlCommandExecutor>().Object;
            _commandFactory = new Mock<ICommandFactory>().Object;
        }

        public DocumentsBuilder WithDbConnection(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            return this;
        }

        public DocumentsBuilder WithSqlCommandExecutor(ISqlCommandExecutor sqlCommandExecutor)
        {
            _sqlCommandExecutor = sqlCommandExecutor;
            return this;
        }

        public DocumentsBuilder WithCommandFactory(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            return this;
        }

        public Documents Build()
        {
            return new Documents(_dbConnection, _sqlCommandExecutor, _commandFactory);
        }
    }

    public class Documents
    {
        private readonly IDbConnection _dbConnection;
        private readonly ISqlCommandExecutor _sqlCommandExecutor;
        private readonly ICommandFactory _commandFactory;
        private readonly IIdService _idService;
        private readonly ITypeService _typeService;
        private readonly ISerializationService _serializationService;

        public Documents(IDbConnection dbConnection, ISqlCommandExecutor sqlCommandExecutor, ICommandFactory commandFactory)
        {
            _dbConnection = dbConnection;
            _sqlCommandExecutor = sqlCommandExecutor;
            _commandFactory = commandFactory;
            _idService = null;
            _typeService = null;
            _serializationService = null;
        }

        public IDbConnection DbConnection
        {
            get { return _dbConnection; }
        }

        public ISqlCommandExecutor SqlCommandExecutor
        {
            get { return _sqlCommandExecutor; }
        }

        public void Save(object document)
        {
            var documentId = _idService.GetIdFrom(document);
            var documentType = _typeService.GetTypeNameFrom(document);
            var id = string.Format("{0}/{1}", documentType, documentId);

            var data = _serializationService.Serialize(document);

            var insertCommand = _commandFactory.CreateInsertCommandFor(id, data, documentType);
            _sqlCommandExecutor.ExecuteNonQuery(_dbConnection, insertCommand);
        }
    }
}