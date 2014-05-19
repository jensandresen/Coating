using System.Data;
using Moq;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestDocuments
    {
        [Test]
        public void current_connection_returns_expected()
        {
            var expectedConnection = new Mock<IDbConnection>().Object;

            var sut = new DocumentsBuilder()
                .WithDbConnection(expectedConnection)
                .Build();

            Assert.AreSame(expectedConnection, sut.DbConnection);
        }
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