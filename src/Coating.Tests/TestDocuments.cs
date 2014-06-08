using System.Linq;
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

        [Test]
        public void retrieve_returns_null_when_nothing_is_found()
        {
            DataDocument nullDataDocument = null;

            var sut = new DocumentsBuilder()
                .WithDatabaseFacade(new StubDatabaseFacade(nullDataDocument))
                .Build();

            var result = sut.Retrieve<object>("dummy id");

            Assert.IsNull(result);
        }

        [Test]
        public void retrieve_deserializes_found_data_document()
        {
            var mockSerializationService = new Mock<ISerializationService>();

            var stubDataDocument = new DataDocument
                {
                    Data = "foo"
                };

            var sut = new DocumentsBuilder()
                .WithDatabaseFacade(new StubDatabaseFacade(stubDataDocument))
                .WithSerializationService(mockSerializationService.Object)
                .Build();

            sut.Retrieve<object>("dummy id");

            mockSerializationService.Verify(x => x.Deserialize<object>(stubDataDocument.Data));
        }

        [Test]
        public void retrieve_returns_deserialized_data()
        {
            var expected = new object();
            var dummyDataDocument = new DataDocument();

            var sut = new DocumentsBuilder()
                .WithSerializationService(new StubSerializationService(deserializationResult: expected))
                .WithDatabaseFacade(new StubDatabaseFacade(dummyDataDocument))
                .Build();

            var result = sut.Retrieve<object>("dummy id");

            Assert.AreSame(expected, result);
        }

        [Test]
        public void retrieveall_returns_empty_list_when_nothing_was_found()
        {
            var sut = new DocumentsBuilder()
                .WithDatabaseFacade(new StubDatabaseFacade(selectByTypeResult: Enumerable.Empty<DataDocument>()))
                .Build();

            var result = sut.RetrieveAll<object>();

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [TestCase("Foo")]
        [TestCase("Bar")]
        [TestCase("Baz")]
        [TestCase("Qux")]
        public void retrieveall_uses_expected_typename_to_retrieve_data_documents(string expectedTypeName)
        {
            var mockDatabaseFacade = new Mock<IDatabaseFacade>();

            var sut = new DocumentsBuilder()
                .WithDatabaseFacade(mockDatabaseFacade.Object)
                .WithTypeService(new StubTypeService(expectedTypeName))
                .Build();

            sut.RetrieveAll<object>().ToArray();

            mockDatabaseFacade.Verify(x => x.SelectByType(expectedTypeName));
        }

        [Test]
        public void retrieveall_deserializes_all_data_documents_found()
        {
            var mockSerializationService = new Mock<ISerializationService>();

            var sut = new DocumentsBuilder()
                .WithSerializationService(mockSerializationService.Object)
                .WithDatabaseFacade(new StubDatabaseFacade(selectByTypeResult: new[]
                    {
                        new DataDocument(),
                        new DataDocument(),
                        new DataDocument(),
                    }))
                .Build();

            sut.RetrieveAll<object>().ToArray();

            mockSerializationService.Verify(x => x.Deserialize<object>(It.IsAny<string>()), Times.Exactly(3));
        }

        [TestCase("Foo", "1", "Foo/1")]
        [TestCase("Bar", "2", "Bar/2")]
        [TestCase("Foo", "Bar", "Foo/Bar")]
        public void contains_uses_expected_id(string documentTypeName, string documentId, string expectedId)
        {
            var mockDatabaseFacade = new Mock<IDatabaseFacade>();

            var sut = new DocumentsBuilder()
                .WithDatabaseFacade(mockDatabaseFacade.Object)
                .WithTypeService(new StubTypeService(documentTypeName))
                .WithIdService(new StubIdService(documentId))
                .Build();

            sut.Contains(new object());

            mockDatabaseFacade.Verify(x => x.Contains(expectedId));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void contains_returns_the_same_as_database_facade(bool expected)
        {
            var sut = new DocumentsBuilder()
                .WithDatabaseFacade(new StubDatabaseFacade(containsResult: expected))
                .Build();

            var actual = sut.Contains(new object());

            Assert.AreEqual(expected, actual);
        }
    }
}