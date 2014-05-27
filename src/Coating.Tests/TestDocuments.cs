using Coating.Tests.Builders;
using Coating.Tests.TestDoubles;
using Moq;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestDocuments
    {
        [Test]
        public void database_facade_returns_expected()
        {
            var dummyDatabaseFacade = new Mock<IDatabaseFacade>().Object;

            var sut = new DocumentsBuilder()
                .WithDatabaseFacade(dummyDatabaseFacade)
                .Build();

            Assert.AreSame(dummyDatabaseFacade, sut.DatabaseFacade);
        }

        [Test]
        public void idservice_returns_expected()
        {
            var dummy = new Mock<IIdService>().Object;

            var sut = new DocumentsBuilder()
                .WithIdService(dummy)
                .Build();

            Assert.AreSame(dummy, sut.IdService);
        }

        [Test]
        public void typeservice_returns_expected()
        {
            var dummy = new Mock<ITypeService>().Object;

            var sut = new DocumentsBuilder()
                .WithTypeService(dummy)
                .Build();

            Assert.AreSame(dummy, sut.TypeService);
        }

        [Test]
        public void serialization_service_returns_expected()
        {
            var dummy = new Mock<ISerializationService>().Object;

            var sut = new DocumentsBuilder()
                .WithSerializationService(dummy)
                .Build();

            Assert.AreSame(dummy, sut.SerializationService);
        }

        [Test]
        public void inserts_data_document_when_saving()
        {
            var mock = new Mock<IDatabaseFacade>();

            var sut = new DocumentsBuilder()
                .WithDatabaseFacade(mock.Object)
                .Build();

            var dummyDocument = new object();
            sut.Save(dummyDocument);

            mock.Verify(x => x.Insert(It.IsAny<DataDocument>()));
        }

        [TestCase("Foo", "1", "Foo/1")]
        [TestCase("Bar", "2", "Bar/2")]
        [TestCase("Foo", "Bar", "Foo/Bar")]
        public void generates_expected_id_from_document_when_saving(string documentTypeName, string documentId, string expectedId)
        {
            var spyDatabaseFacade = new SpyDatabaseFacade();

            var sut = new DocumentsBuilder()
                .WithIdService(new StubIdService(documentId))
                .WithTypeService(new StubTypeService(documentTypeName))
                .WithDatabaseFacade(spyDatabaseFacade)
                .Build();

            var dummyDocument = new object();
            sut.Save(dummyDocument);

            Assert.AreEqual(expectedId, spyDatabaseFacade.insertedDocument.Id);
        }

        [TestCase("Foo")]
        [TestCase("Bar")]
        [TestCase("Baz")]
        [TestCase("Qux")]
        public void generates_expected_typename_from_document_when_saving(string expectedTypeName)
        {
            var spyDatabaseFacade = new SpyDatabaseFacade();

            var sut = new DocumentsBuilder()
                .WithTypeService(new StubTypeService(expectedTypeName))
                .WithDatabaseFacade(spyDatabaseFacade)
                .Build();

            var dummyDocument = new object();
            sut.Save(dummyDocument);

            Assert.AreEqual(expectedTypeName, spyDatabaseFacade.insertedDocument.Type);
        }

        [TestCase("Foo")]
        [TestCase("Bar")]
        [TestCase("Baz")]
        [TestCase("Qux")]
        public void generates_expected_data_from_document_when_saving(string expectedData)
        {
            var spyDatabaseFacade = new SpyDatabaseFacade();

            var sut = new DocumentsBuilder()
                .WithSerializationService(new StubSerializationService(expectedData))
                .WithDatabaseFacade(spyDatabaseFacade)
                .Build();

            var dummyDocument = new object();
            sut.Save(dummyDocument);

            Assert.AreEqual(expectedData, spyDatabaseFacade.insertedDocument.Data);
        }
    }
}