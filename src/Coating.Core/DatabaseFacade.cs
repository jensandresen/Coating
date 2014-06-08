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
            var results = new LinkedList<DataDocument>();

            var cmd = _commandFactory.CreateSelectByIdCommandFor(id);
            _commandExecutor.ExecuteReadCommand(cmd, r => results.AddLast(new DataDocument
                {
                    Id = r.GetString("Id"),
                    Data = r.GetString("Data"),
                    Type = r.GetString("Type"),
                }));

            return results.SingleOrDefault();
        }

        public IEnumerable<DataDocument> SelectByType(string typeName)
        {
            var results = new LinkedList<DataDocument>();

            var cmd = _commandFactory.CreateSelectByTypeCommandFor(typeName);
            _commandExecutor.ExecuteReadCommand(cmd, r => results.AddLast(new DataDocument
            {
                Id = r.GetString("Id"),
                Data = r.GetString("Data"),
                Type = r.GetString("Type"),
            }));

            return results;
        }

        public bool Contains(string id)
        {
            return SelectById(id) != null;
        }

        public void Delete(string id)
        {
            var cmd = _commandFactory.CreateDeleteCommandFor(id);
            _commandExecutor.ExecuteWriteCommand(cmd);
        }
    }
}