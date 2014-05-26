using System.Data;
using Coating.Tests.Builders;
using Coating.Tests.TestDoubles;
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

            var stubDocument = new DataDocument
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
            
            var dummyDocument = new DataDocument();
            var dummyInsertCommand = new SqlCommandBuilder().Build();

            var sut = new DatabaseFacadeBuilder()
                .WithCommandFactory(new StubCommandFactory(insert: dummyInsertCommand))
                .WithExecutor(mock.Object)
                .Build();

            sut.Insert(dummyDocument);

            mock.Verify(x => x.ExecuteWriteCommand(dummyInsertCommand));
        }
    }
}