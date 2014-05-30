using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestConstruction
    {
        private const string ConnectionString = "Server=.;Database=Coating;Trusted_Connection=True;";

        [Test]
        public void METHOD_NAME()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                ICommandFactory commandFactory = new CommandFactory("Data");
                ICommandMapper mapper = new DefaultCommandMapper();
                ICommandExecutor commandExecutor = new TransactionalCommandExecutor(connection, mapper);
                IDatabaseFacade databaseFacade = new DatabaseFacade(commandFactory, commandExecutor);

                IIdService idService = new AutoIdService("Id");
                ITypeService typeService = new TypeService();
                ISerializationService serializationService = new JsonSerializationService();
                IDocuments documents = new Documents(databaseFacade, idService, typeService, serializationService);

                documents.Store(new Person
                    {
                        Id = "1",
                        Name = "John Doe",
                        Age = 16
                    });
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