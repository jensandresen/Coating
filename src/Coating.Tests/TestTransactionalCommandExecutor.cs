using System;
using System.Data;
using Coating.Tests.Builders;
using Coating.Tests.TestDoubles;
using Moq;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestTransactionalCommandExecutor
    {
        [Test]
        public void write_begins_a_new_transation()
        {
            var dummyTransaction = new Mock<IDbTransaction>().Object;
            var dummyDbCommand = new Mock<IDbCommand>().Object;
            var dummyCommand = new SqlCommand();

            var mock = new Mock<IDbConnection>();
            mock
                .Setup(x => x.BeginTransaction())
                .Returns(dummyTransaction);

            var sut = new TransactionalCommandExecutorBuilder()
                .WithConnection(mock.Object)
                .WithMapper(new StubCommandMapper(dummyDbCommand))
                .Build();

            sut.ExecuteWriteCommand(dummyCommand);

            mock.Verify(x => x.BeginTransaction());
        }

        [Test]
        public void write_commits_the_transaction()
        {
            var mock = new Mock<IDbTransaction>();
            var dummyDbCommand = new Mock<IDbCommand>().Object;
            var dummyCommand = new SqlCommand();

            var sut = new TransactionalCommandExecutorBuilder()
                .WithConnection(new StubDbConnection(mock.Object))
                .WithMapper(new StubCommandMapper(dummyDbCommand))
                .Build();

            sut.ExecuteWriteCommand(dummyCommand);

            mock.Verify(x => x.Commit());
        }

        [Test]
        public void write_lets_exceptions_propegate()
        {
            var dummyConnection = new Mock<IDbTransaction>().Object;
            var dummyCommand = new SqlCommand();

            var temp = new Mock<IDbCommand>();
            temp
                .Setup(x => x.ExecuteNonQuery())
                .Callback(() => { throw new NonQueryTestException(); });

            var failingDbCommand = temp.Object;

            var sut = new TransactionalCommandExecutorBuilder()
                .WithConnection(new StubDbConnection(dummyConnection))
                .WithMapper(new StubCommandMapper(failingDbCommand))
                .Build();

            Assert.Throws<NonQueryTestException>(() => sut.ExecuteWriteCommand(dummyCommand));
        }

        [Test]
        public void write_rolls_back_transaction_if_command_execution_fails()
        {
            var mock = new Mock<IDbTransaction>();
            var dummyCommand = new SqlCommand();

            var temp = new Mock<IDbCommand>();
            temp
                .Setup(x => x.ExecuteNonQuery())
                .Callback(() => { throw new NonQueryTestException(); });

            var failingDbCommand = temp.Object;

            var sut = new TransactionalCommandExecutorBuilder()
                .WithConnection(new StubDbConnection(mock.Object))
                .WithMapper(new StubCommandMapper(failingDbCommand))
                .Build();

            try
            {
                sut.ExecuteWriteCommand(dummyCommand);
            }
            catch (NonQueryTestException)
            {

            }

            mock.Verify(x => x.Rollback());
        }

        [Test]
        public void write_disposes_transaction_on_success()
        {
            var mock = new Mock<IDbTransaction>();
            var dummyDbCommand = new Mock<IDbCommand>().Object;
            var dummyCommand = new SqlCommand();

            var sut = new TransactionalCommandExecutorBuilder()
                .WithConnection(new StubDbConnection(mock.Object))
                .WithMapper(new StubCommandMapper(dummyDbCommand))
                .Build();

            sut.ExecuteWriteCommand(dummyCommand);

            mock.Verify(x => x.Dispose());
        }

        [Test]
        public void write_disposes_transaction_on_exception()
        {
            var mock = new Mock<IDbTransaction>();
            var dummyCommand = new SqlCommand();

            var temp = new Mock<IDbCommand>();
            temp
                .Setup(x => x.ExecuteNonQuery())
                .Callback(() => { throw new NonQueryTestException(); });

            var failingDbCommand = temp.Object;

            var sut = new TransactionalCommandExecutorBuilder()
                .WithConnection(new StubDbConnection(mock.Object))
                .WithMapper(new StubCommandMapper(failingDbCommand))
                .Build();

            try
            {
                sut.ExecuteWriteCommand(dummyCommand);
            }
            catch (NonQueryTestException)
            {

            }

            mock.Verify(x => x.Dispose());
        }

        private class NonQueryTestException : Exception
        {
            
        }
    }
}