﻿using System;
using System.Data;
using Coating.Tests.Builders;
using Coating.Tests.TestDoubles;
using Moq;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestStorageFacade
    {
        [Test]
        public void creates_expected_command_for_insert()
        {
            var mockCommandFactory = new Mock<ICommandFactory>();

            var sut = new StorageFacadeBuilder()
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
            var dummyCommand = new SqlCommandBuilder().Build();

            var sut = new StorageFacadeBuilder()
                .WithCommandFactory(new StubCommandFactory(insert: dummyCommand))
                .WithExecutor(mock.Object)
                .Build();

            sut.Insert(dummyDocument);

            mock.Verify(x => x.ExecuteWriteCommand(dummyCommand));
        }

        [Test]
        public void creates_expected_command_for_update()
        {
            var mockCommandFactory = new Mock<ICommandFactory>();

            var sut = new StorageFacadeBuilder()
                .WithCommandFactory(mockCommandFactory.Object)
                .Build();

            var stubDocument = new DataDocument
            {
                Id = "1",
                Data = "foo",
                Type = "bar"
            };

            sut.Update(stubDocument);

            mockCommandFactory.Verify(x => x.CreateUpdateCommandFor(stubDocument.Id, stubDocument.Data, stubDocument.Type));
        }

        [Test]
        public void executes_update_command()
        {
            var mock = new Mock<ICommandExecutor>();

            var dummyDocument = new DataDocument();
            var dummyCommand = new SqlCommandBuilder().Build();

            var sut = new StorageFacadeBuilder()
                .WithCommandFactory(new StubCommandFactory(update: dummyCommand))
                .WithExecutor(mock.Object)
                .Build();

            sut.Update(dummyDocument);

            mock.Verify(x => x.ExecuteWriteCommand(dummyCommand));
        }

        [Test]
        public void executes_select_by_id_command()
        {
            var mock = new Mock<ICommandExecutor>();

            var dummyCommand = new SqlCommandBuilder().Build();

            var sut = new StorageFacadeBuilder()
                .WithCommandFactory(new StubCommandFactory(selectById: dummyCommand))
                .WithExecutor(mock.Object)
                .Build();

            sut.SelectById("dummy id");

            mock.Verify(x => x.ExecuteReadCommand(dummyCommand, It.IsAny<Action<IDataRecord>>()));
        }

        [Test]
        public void executes_select_by_type_command()
        {
            var mock = new Mock<ICommandExecutor>();

            var dummyCommand = new SqlCommandBuilder().Build();

            var sut = new StorageFacadeBuilder()
                .WithCommandFactory(new StubCommandFactory(selectByType: dummyCommand))
                .WithExecutor(mock.Object)
                .Build();

            sut.SelectByType("dummy type name");

            mock.Verify(x => x.ExecuteReadCommand(dummyCommand, It.IsAny<Action<IDataRecord>>()));
        }

        [Test]
        public void executes_delete_command()
        {
            var mock = new Mock<ICommandExecutor>();

            var dummyCommand = new SqlCommandBuilder().Build();

            var sut = new StorageFacadeBuilder()
                .WithCommandFactory(new StubCommandFactory(delete: dummyCommand))
                .WithExecutor(mock.Object)
                .Build();

            sut.Delete("dummy id");

            mock.Verify(x => x.ExecuteWriteCommand(dummyCommand));
        }

        [Test]
        public void contains_returns_expected_when_nothing_was_found()
        {
            var sut = new StorageFacadeBuilder().Build();
            var result = sut.Contains("non-existing-id");

            Assert.IsFalse(result);
        }

        [Test]
        public void contains_returns_expected_when_a_document_was_found()
        {
            var dummyDataRecord = new Mock<IDataRecord>().Object;

            var sut = new StorageFacadeBuilder()
                .WithExecutor(new StubCommandExecutor(readResult: dummyDataRecord))
                .Build();

            var result = sut.Contains("dummy id");

            Assert.IsTrue(result);
        }
    }
}