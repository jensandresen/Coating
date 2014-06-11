using System.Linq;
using Coating.Tests.Builders;
using Coating.Tests.TestDoubles;
using Moq;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestDocumentStore
    {
        [Test]
        public void storage_facade_returns_expected()
        {
            var dummyStorageFacade = new Mock<IStorageFacade>().Object;

            var sut = new DocumentStoreBuilder()
                .WithStorageFacade(dummyStorageFacade)
                .Build();

            Assert.AreSame(dummyStorageFacade, sut.StorageFacade);
        }

        [Test]
        public void idservice_returns_expected()
        {
            var dummy = new Mock<IIdService>().Object;

            var sut = new DocumentStoreBuilder()
                .WithIdService(dummy)
                .Build();

            Assert.AreSame(dummy, sut.IdService);
        }

        [Test]
        public void typeservice_returns_expected()
        {
            var dummy = new Mock<ITypeService>().Object;

            var sut = new DocumentStoreBuilder()
                .WithTypeService(dummy)
                .Build();

            Assert.AreSame(dummy, sut.TypeService);
        }

        [Test]
        public void serialization_service_returns_expected()
        {
            var dummy = new Mock<ISerializationService>().Object;

            var sut = new DocumentStoreBuilder()
                .WithSerializationService(dummy)
                .Build();

            Assert.AreSame(dummy, sut.SerializationService);
        }

        [Test]
        public void inserts_data_document_when_saving()
        {
            var mock = new Mock<IStorageFacade>();

            var sut = new DocumentStoreBuilder()
                .WithStorageFacade(mock.Object)
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
            var spyStorageFacade = new SpyStorageFacade();

            var sut = new DocumentStoreBuilder()
                .WithIdService(new StubIdService(documentId))
                .WithTypeService(new StubTypeService(documentTypeName))
                .WithStorageFacade(spyStorageFacade)
                .Build();

            var dummyDocument = new object();
            sut.Save(dummyDocument);

            Assert.AreEqual(expectedId, spyStorageFacade.insertedDocument.Id);
        }

        [TestCase("Foo")]
        [TestCase("Bar")]
        [TestCase("Baz")]
        [TestCase("Qux")]
        public void generates_expected_typename_from_document_when_saving(string expectedTypeName)
        {
            var spyStorageFacade = new SpyStorageFacade();

            var sut = new DocumentStoreBuilder()
                .WithTypeService(new StubTypeService(expectedTypeName))
                .WithStorageFacade(spyStorageFacade)
                .Build();

            var dummyDocument = new object();
            sut.Save(dummyDocument);

            Assert.AreEqual(expectedTypeName, spyStorageFacade.insertedDocument.Type);
        }

        [TestCase("Foo")]
        [TestCase("Bar")]
        [TestCase("Baz")]
        [TestCase("Qux")]
        public void generates_expected_data_from_document_when_saving(string expectedData)
        {
            var spyStorageFacade = new SpyStorageFacade();

            var sut = new DocumentStoreBuilder()
                .WithSerializationService(new StubSerializationService(expectedData))
                .WithStorageFacade(spyStorageFacade)
                .Build();

            var dummyDocument = new object();
            sut.Save(dummyDocument);

            Assert.AreEqual(expectedData, spyStorageFacade.insertedDocument.Data);
        }

        [Test]
        public void retrieve_returns_null_when_nothing_is_found()
        {
            DataDocument nullDataDocument = null;

            var sut = new DocumentStoreBuilder()
                .WithStorageFacade(new StubStorageFacade(nullDataDocument))
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

            var sut = new DocumentStoreBuilder()
                .WithStorageFacade(new StubStorageFacade(stubDataDocument))
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

            var sut = new DocumentStoreBuilder()
                .WithSerializationService(new StubSerializationService(deserializationResult: expected))
                .WithStorageFacade(new StubStorageFacade(dummyDataDocument))
                .Build();

            var result = sut.Retrieve<object>("dummy id");

            Assert.AreSame(expected, result);
        }

        [Test]
        public void retrieveall_returns_empty_list_when_nothing_was_found()
        {
            var sut = new DocumentStoreBuilder()
                .WithStorageFacade(new StubStorageFacade(selectByTypeResult: Enumerable.Empty<DataDocument>()))
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
            var mockStorageFacade = new Mock<IStorageFacade>();

            var sut = new DocumentStoreBuilder()
                .WithStorageFacade(mockStorageFacade.Object)
                .WithTypeService(new StubTypeService(expectedTypeName))
                .Build();

            sut.RetrieveAll<object>().ToArray();

            mockStorageFacade.Verify(x => x.SelectByType(expectedTypeName));
        }

        [Test]
        public void retrieveall_deserializes_all_data_documents_found()
        {
            var mockSerializationService = new Mock<ISerializationService>();

            var sut = new DocumentStoreBuilder()
                .WithSerializationService(mockSerializationService.Object)
                .WithStorageFacade(new StubStorageFacade(selectByTypeResult: new[]
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
            var mockStorageFacade = new Mock<IStorageFacade>();

            var sut = new DocumentStoreBuilder()
                .WithStorageFacade(mockStorageFacade.Object)
                .WithTypeService(new StubTypeService(documentTypeName))
                .WithIdService(new StubIdService(documentId))
                .Build();

            sut.Contains(new object());

            mockStorageFacade.Verify(x => x.Contains(expectedId));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void contains_returns_the_same_as_database_facade(bool expected)
        {
            var sut = new DocumentStoreBuilder()
                .WithStorageFacade(new StubStorageFacade(containsResult: expected))
                .Build();

            var actual = sut.Contains(new object());

            Assert.AreEqual(expected, actual);
        }

        [TestCase("Foo", "1", "Foo/1")]
        [TestCase("Bar", "2", "Bar/2")]
        [TestCase("Foo", "Bar", "Foo/Bar")]
        public void delete_uses_expected_id(string documentTypeName, string documentId, string expectedId)
        {
            var mockStorageFacade = new Mock<IStorageFacade>();

            var sut = new DocumentStoreBuilder()
                .WithStorageFacade(mockStorageFacade.Object)
                .WithTypeService(new StubTypeService(documentTypeName))
                .WithIdService(new StubIdService(documentId))
                .Build();

            sut.Delete<object>(documentId);

            mockStorageFacade.Verify(x => x.Delete(expectedId));
        }

        [Test]
        public void store_only_updates_document_if_it_already_exists()
        {
            var mockStorageFacade = new Mock<IStorageFacade>();
            mockStorageFacade
                .Setup(x => x.Contains(It.IsAny<string>()))
                .Returns(true);

            var sut = new DocumentStoreBuilder()
                .WithStorageFacade(mockStorageFacade.Object)
                .Build();

            sut.Store(new object());

            mockStorageFacade.Verify(x => x.Update(It.IsAny<DataDocument>()));
        }

        [Test]
        public void store_inserts_document_if_it_does_not_already_exists()
        {
            var mockStorageFacade = new Mock<IStorageFacade>();
            mockStorageFacade
                .Setup(x => x.Contains(It.IsAny<string>()))
                .Returns(false);

            var sut = new DocumentStoreBuilder()
                .WithStorageFacade(mockStorageFacade.Object)
                .Build();

            sut.Store(new object());

            mockStorageFacade.Verify(x => x.Insert(It.IsAny<DataDocument>()));
        }
    }
}