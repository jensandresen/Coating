using System.Collections.Generic;
using System.Linq;

namespace Coating
{
    public class DatabaseFacade : IDatabaseFacade
    {
        private readonly ICommandFactory _commandFactory;
        private readonly ICommandExecutor _commandExecutor;

        public DatabaseFacade(ICommandFactory commandFactory, ICommandExecutor commandExecutor)
        {
            _commandFactory = commandFactory;
            _commandExecutor = commandExecutor;
        }

        public void Insert(DataDocument document)
        {
            var cmd = _commandFactory.CreateInsertCommandFor(document.Id, document.Data, document.Type);
            _commandExecutor.ExecuteWriteCommand(cmd);
        }

        public void Update(DataDocument document)
        {
            var cmd = _commandFactory.CreateUpdateCommandFor(document.Id, document.Data, document.Type);
            _commandExecutor.ExecuteWriteCommand(cmd);
        }

        public DataDocument SelectById(string id)
        {
            var cmd = _commandFactory.CreateSelectByIdCommandFor(id);
            var records = _commandExecutor.ExecuteReadCommand(cmd);

            var found = records.SingleOrDefault();

            if (found == null)
            {
                return null;
            }

            return new DataDocument
                {
                    Id = found.GetString("Id"),
                    Data = found.GetString("Data"),
                    Type = found.GetString("Type"),
                };
        }

        public IEnumerable<DataDocument> SelectByType(string typeName)
        {
            var cmd = _commandFactory.CreateSelectByTypeCommandFor(typeName);
            var records = _commandExecutor.ExecuteReadCommand(cmd);

            foreach (var record in records)
            {
                yield return new DataDocument
                {
                    Id = record.GetString("Id"),
                    Data = record.GetString("Data"),
                    Type = record.GetString("Type"),
                };
            }
        }
    }
}