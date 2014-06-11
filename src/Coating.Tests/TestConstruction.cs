using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture, Ignore]
    public class TestConstruction
    {
        private const string ConnectionString = "Server=.;Database=Coating;Trusted_Connection=True;";

        [Test]
        public void end_to_end_store_and_retrieve()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                ICommandFactory commandFactory = new CommandFactory("Data");
                ICommandMapper mapper = new DefaultCommandMapper();
                ICommandExecutor commandExecutor = new TransactionalCommandExecutor(connection, mapper);
                IStorageFacade storageFacade = new StorageFacade(commandFactory, commandExecutor);

                IIdService idService = new ConventionBasedIdService("Id");
                ITypeService typeService = new TypeService();
                ISerializationService serializationService = new JsonSerializationService();
                IDocumentStore documentStore = new DocumentStore(storageFacade, idService, typeService, serializationService);

                documentStore.Store(new Person
                    {
                        Id = "1",
                        Name = "John Doe",
                        Age = 16
                    });

                var storedPerson = documentStore.Retrieve<Person>("1");

                Assert.IsNotNull(storedPerson);
                Assert.AreEqual("1", storedPerson.Id);
                Assert.AreEqual("John Doe", storedPerson.Name);
                Assert.AreEqual(16, storedPerson.Age);

                documentStore.Delete<Person>("1");

                storedPerson = documentStore.Retrieve<Person>("1");
                Assert.IsNull(storedPerson);
            }
        }

        private class Person
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}