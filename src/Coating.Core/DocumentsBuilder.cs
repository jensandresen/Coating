namespace Coating
{
//    public class DocumentsBuilder
//    {
//        private IDatabaseFacade _databaseFacade;
//        private IIdService _idService;
//        private ITypeService _typeService;
//        private ISerializationService _serializationService;
//        private ICommandFactory _commandFactory;
//        private ICommandExecutor _commandExecutor;
//        private string _tableName;
//
//        private void FillHoles()
//        {
//            if (_idService == null)
//            {
//                _idService = new AutoIdService();
//            }
//
//            if (_typeService == null)
//            {
//                _typeService = new TypeService();
//            }
//
//            if (_serializationService == null)
//            {
//                _serializationService = new JsonSerializationService();
//            }
//
//            if (_tableName == null)
//            {
//                _tableName = "Data";
//            }
//
//            if (_commandFactory == null)
//            {
//                _commandFactory = new CommandFactory(_tableName);
//            }
//
//            if (_commandExecutor == null)
//            {
//                _commandExecutor = new TransactionalCommandExecutor(null, new );
//            }
//
//            if (_databaseFacade == null)
//            {
//                _databaseFacade = new DatabaseFacade(_commandFactory, _commandExecutor);
//            }
//        }
//
//        public IDocuments Build()
//        {
//            FillHoles();
//            return new Documents(_databaseFacade, _idService, _typeService, _serializationService);
//        }
//    }
//
//    public class DatabaseFacadeBuilder
//    {
//        private ICommandFactory _commandFactory;
//        private ICommandExecutor _commandExecutor;
//
//        public DatabaseFacadeBuilder WithCommandFactory(ICommandFactory commandFactory)
//        {
//            _commandFactory = commandFactory;
//            return this;
//        }
//
//        public DatabaseFacadeBuilder WithCommandExecutor(ICommandExecutor commandExecutor)
//        {
//            _commandExecutor = commandExecutor;
//            return this;
//        }
//
//        public DatabaseFacade Build()
//        {
//            ApplyDefaults();
//            return new DatabaseFacade(_commandFactory, _commandExecutor);
//        }
//
//        private void ApplyDefaults()
//        {
//            if (_commandFactory == null)
//            {
//                _commandFactory = new CommandFactory();
//            }
//        }
//    }
}