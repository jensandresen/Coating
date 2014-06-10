using System;
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
        public void disposes_ado_command_on_write()
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
        public void assigns_connection_before_executing_the_write_command()
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

        [Test]
        public void executes_reader()
        {
            var mockCommand = new Mock<IDbCommand>();

            var sut = new DefaultCommandExecutorBuilder()
                .WithMapper(new StubCommandMapper(mockCommand.Object))
                .Build();

            var dummyCommand = new SqlCommandBuilder().Build();
            var dummyCallback = new Action<IDataRecord>(record => { });
            
            sut.ExecuteReadCommand(dummyCommand, dummyCallback);

            mockCommand.Verify(x => x.ExecuteReader());
        }

        [Test]
        public void assigns_connection_before_executing_read_command()
        {
            var dummyDbConnection = new Mock<IDbConnection>().Object;
            var spyDbCommand = new SpyDbCommand();

            var sut = new DefaultCommandExecutorBuilder()
                .WithDbConnection(dummyDbConnection)
                .WithMapper(new StubCommandMapper(spyDbCommand))
                .Build();

            var dummyCommand = new SqlCommandBuilder().Build();
            var dummyCallback = new Action<IDataRecord>(record => { });

            sut.ExecuteReadCommand(dummyCommand, dummyCallback);

            Assert.AreSame(dummyDbConnection, spyDbCommand.assignedConnection);
        }

        [Test]
        public void disposes_ado_command_on_read()
        {
            var mockAdoCommand = new Mock<IDbCommand>();

            var sut = new DefaultCommandExecutorBuilder()
                .WithMapper(new StubCommandMapper(mockAdoCommand.Object))
                .Build();

            var dummyCommand = new SqlCommandBuilder().Build();
            var dummyCallback = new Action<IDataRecord>(record => { });

            sut.ExecuteReadCommand(dummyCommand, dummyCallback);

            mockAdoCommand.Verify(x => x.Dispose());
        }

        [Test]
        public void executes_callback_for_single_result()
        {
            var dummyDataReader = new FakeDataReader();
            
            var stubCommand = new Mock<IDbCommand>();
            stubCommand
                .Setup(x => x.ExecuteReader())
                .Returns(dummyDataReader);

            var sut = new DefaultCommandExecutorBuilder()
                .WithMapper(new StubCommandMapper(stubCommand.Object))
                .Build();

            bool wasCallbackInvoked = false;
            IDataRecord returnedRecord = null;

            var spyCallback = new Action<IDataRecord>(record =>
                {
                    wasCallbackInvoked = true;
                    returnedRecord = record;
                });

            var dummyCommand = new SqlCommandBuilder().Build();
            sut.ExecuteReadCommand(dummyCommand, spyCallback);

            Assert.IsTrue(wasCallbackInvoked);
            Assert.AreSame(dummyDataReader, returnedRecord);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(100)]
        public void executes_callback_for_each_result_found(int expectedCallbackCount)
        {
            var dummyDataReader = new FakeDataReader(resultRowCount: expectedCallbackCount);
            
            var stubCommand = new Mock<IDbCommand>();
            stubCommand
                .Setup(x => x.ExecuteReader())
                .Returns(dummyDataReader);

            var sut = new DefaultCommandExecutorBuilder()
                .WithMapper(new StubCommandMapper(stubCommand.Object))
                .Build();

            var callbackCount = 0;

            var spyCallback = new Action<IDataRecord>(record =>
                {
                    callbackCount++;
                });

            var dummyCommand = new SqlCommandBuilder().Build();
            sut.ExecuteReadCommand(dummyCommand, spyCallback);

            Assert.AreEqual(expectedCallbackCount, callbackCount);
        }

        [Test]
        public void does_not_execute_callback_if_nothing_was_found()
        {
            var dummyDataReader = new FakeDataReader(resultRowCount: 0);

            var stubCommand = new Mock<IDbCommand>();
            stubCommand
                .Setup(x => x.ExecuteReader())
                .Returns(dummyDataReader);

            var sut = new DefaultCommandExecutorBuilder()
                .WithMapper(new StubCommandMapper(stubCommand.Object))
                .Build();

            var wasCallbackInvoked = false;

            var spyCallback = new Action<IDataRecord>(record =>
            {
                wasCallbackInvoked = true;
            });

            var dummyCommand = new SqlCommandBuilder().Build();
            sut.ExecuteReadCommand(dummyCommand, spyCallback);

            Assert.IsFalse(wasCallbackInvoked);
        }

        [Test]
        public void does_not_execute_callback_if_null_was_returned()
        {
            IDataReader nullDataReader = null;

            var stubCommand = new Mock<IDbCommand>();
            stubCommand
                .Setup(x => x.ExecuteReader())
                .Returns(nullDataReader);

            var sut = new DefaultCommandExecutorBuilder()
                .WithMapper(new StubCommandMapper(stubCommand.Object))
                .Build();

            var wasCallbackInvoked = false;

            var spyCallback = new Action<IDataRecord>(record =>
            {
                wasCallbackInvoked = true;
            });

            var dummyCommand = new SqlCommandBuilder().Build();
            sut.ExecuteReadCommand(dummyCommand, spyCallback);

            Assert.IsFalse(wasCallbackInvoked);
        }

    }
}