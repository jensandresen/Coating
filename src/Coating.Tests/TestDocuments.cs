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
        public void end_to_end_with_default_settings()
        {
        }

        private class Foo
        {
            public string Id { get; set; }
            public string Key { get; set; }
        }
    }

    public class SpyDbConnection : IDbConnection
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IDbTransaction BeginTransaction()
        {
            throw new System.NotImplementedException();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new System.NotImplementedException();
        }

        public IDbCommand CreateCommand()
        {
            throw new System.NotImplementedException();
        }

        public void Open()
        {
            throw new System.NotImplementedException();
        }

        public string ConnectionString { get; set; }
        public int ConnectionTimeout { get; private set; }
        public string Database { get; private set; }
        public ConnectionState State { get; private set; }
    }

    public class DocumentsBuilder
    {
        private IDbConnection _dbConnection;

        public DocumentsBuilder()
        {
            _dbConnection = new Mock<IDbConnection>().Object;
        }

        public DocumentsBuilder WithDbConnection(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            return this;
        }

        public Documents Build()
        {
            return new Documents(_dbConnection);
        }
    }

    public class Documents
    {
        private readonly IDbConnection _dbConnection;

        public Documents(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IDbConnection DbConnection
        {
            get { return _dbConnection; }
        }
    }
}