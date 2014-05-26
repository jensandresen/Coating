using System.Data;
using Coating.Tests.Builders;
using Coating.Tests.TestDoubles;
using Moq;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestDefaultCommandExecutor
    {
        [Test]
        public void is_command_executor()
        {
            var sut = new DefaultCommandExecutorBuilder().Build();
            Assert.IsInstanceOf<ICommandExecutor>(sut);
        }

        [Test]
        public void converts_to_ado_command()
        {
            var dummyDbCommand = new Mock<IDbCommand>().Object;

            var mockCommandMapper = new Mock<ICommandMapper>();
            mockCommandMapper
                .Setup(x => x.Map(It.IsAny<SqlCommand>()))
                .Returns(dummyDbCommand);

            var sut = new DefaultCommandExecutorBuilder()
                .WithMapper(mockCommandMapper.Object)
                .Build();

            var dummyCommand = new SqlCommandBuilder().Build();
            sut.ExecuteWriteCommand(dummyCommand);

            mockCommandMapper.Verify(x => x.Map(dummyCommand));
        }

        [Test]
        public void disposes_ado_command()
        {
            var mockAdoCommand = new Mock<IDbCommand>();

            var sut = new DefaultCommandExecutorBuilder()
                .WithMapper(new StubCommandMapper(mockAdoCommand.Object))
                .Build();

            var dummyCommand = new SqlCommandBuilder().Build();
            sut.ExecuteWriteCommand(dummyCommand);

            mockAdoCommand.Verify(x => x.Dispose());
        }

        [Test]
        public void assigns_connection_before_executing_the_ado_command()
        {
            var dummyDbConnection = new Mock<IDbConnection>().Object;
            var spyDbCommand = new SpyDbCommand();
            
            var sut = new DefaultCommandExecutorBuilder()
                .WithDbConnection(dummyDbConnection)
                .WithMapper(new StubCommandMapper(spyDbCommand))
                .Build();

            var dummyCommand = new SqlCommandBuilder().Build();
            sut.ExecuteWriteCommand(dummyCommand);

            Assert.AreSame(dummyDbConnection, spyDbCommand.assignedConnection);
        }

        [Test]
        public void executes_the_command()
        {
            var mockCommand = new Mock<IDbCommand>();

            var sut = new DefaultCommandExecutorBuilder()
                .WithMapper(new StubCommandMapper(mockCommand.Object))
                .Build();

            var dummyCommand = new SqlCommandBuilder().Build();
            sut.ExecuteWriteCommand(dummyCommand);

            mockCommand.Verify(x => x.ExecuteNonQuery());
        }
    }
}