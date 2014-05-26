using System.Data;

namespace Coating
{
    public class Documents
    {
        private readonly IDbConnection _dbConnection;
        private readonly ICommandExecutor _commandExecutor;
        private readonly ICommandFactory _commandFactory;
        private readonly IIdService _idService;
        private readonly ITypeService _typeService;
        private readonly ISerializationService _serializationService;

        public Documents(IDbConnection dbConnection, ICommandExecutor commandExecutor, ICommandFactory commandFactory)
        {
            _dbConnection = dbConnection;
            _commandExecutor = commandExecutor;
            _commandFactory = commandFactory;
            _idService = null;
            _typeService = null;
            _serializationService = null;
        }

        public IDbConnection DbConnection
        {
            get { return _dbConnection; }
        }

        public ICommandExecutor CommandExecutor
        {
            get { return _commandExecutor; }
        }

        public void Save(object document)
        {
            var documentId = _idService.GetIdFrom(document);
            var documentType = _typeService.GetTypeNameFrom(document);
            var id = string.Format("{0}/{1}", documentType, documentId);

            var data = _serializationService.Serialize(document);

            var insertCommand = _commandFactory.CreateInsertCommandFor(id, data, documentType);
            _commandExecutor.ExecuteWriteCommand(insertCommand);
        }
    }
}