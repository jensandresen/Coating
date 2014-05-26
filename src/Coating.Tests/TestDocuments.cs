using System.Data;
using Coating.Tests.Builders;
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
            var expectedExecutor = new Mock<ICommandExecutor>().Object;

            var sut = new DocumentsBuilder()
                .WithCommandExecutor(expectedExecutor)
                .Build();

            Assert.AreSame(expectedExecutor, sut.CommandExecutor);
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
    }
}