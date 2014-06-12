using System.Data;

namespace Coating
{
    public static class DocumentStoreFactory
    {
        public static IDocumentStore Create(IDbConnection connection, string tableName)
        {
            ICommandFactory commandFactory = new CommandFactory(tableName);
            ICommandMapper commandMapper = new DefaultCommandMapper();
            ICommandExecutor commandExecutor = new TransactionalCommandExecutor(connection, commandMapper);
            IStorageFacade storageFacade = new StorageFacade(commandFactory, commandExecutor);
            IIdService idService = new ConventionBasedIdService();
            ITypeService typeService = new TypeService();
            ISerializationService serializationService = new JsonSerializationService();

            return new DocumentStore(storageFacade, idService, typeService, serializationService);
        }

        public static IDocumentStore Create(IDbConnection connection, string tableName, IIdService idService)
        {
            ICommandFactory commandFactory = new CommandFactory(tableName);
            ICommandMapper commandMapper = new DefaultCommandMapper();
            ICommandExecutor commandExecutor = new TransactionalCommandExecutor(connection, commandMapper);
            IStorageFacade storageFacade = new StorageFacade(commandFactory, commandExecutor);
            ITypeService typeService = new TypeService();
            ISerializationService serializationService = new JsonSerializationService();

            return new DocumentStore(storageFacade, idService, typeService, serializationService);
        }

        public static IDocumentStore Create(IDbConnection connection, ICommandFactory commandFactory, ICommandMapper commandMapper)
        {
            ICommandExecutor commandExecutor = new TransactionalCommandExecutor(connection, commandMapper);
            IStorageFacade storageFacade = new StorageFacade(commandFactory, commandExecutor);
            IIdService idService = new ConventionBasedIdService();
            ITypeService typeService = new TypeService();
            ISerializationService serializationService = new JsonSerializationService();

            return new DocumentStore(storageFacade, idService, typeService, serializationService);
        }

        public static IDocumentStore Create(IDbConnection connection, ICommandFactory commandFactory, ICommandMapper commandMapper, IIdService idService)
        {
            ICommandExecutor commandExecutor = new TransactionalCommandExecutor(connection, commandMapper);
            IStorageFacade storageFacade = new StorageFacade(commandFactory, commandExecutor);
            ITypeService typeService = new TypeService();
            ISerializationService serializationService = new JsonSerializationService();

            return new DocumentStore(storageFacade, idService, typeService, serializationService);
        }

        public static IDocumentStore Create(IStorageFacade storageFacade)
        {
            IIdService idService = new ConventionBasedIdService();

            return Create(storageFacade, idService);
        }

        public static IDocumentStore Create(IStorageFacade storageFacade, IIdService idService)
        {
            ITypeService typeService = new TypeService();
            ISerializationService serializationService = new JsonSerializationService();

            return new DocumentStore(storageFacade, idService, typeService, serializationService);
        }
    }
}