using System.Collections.Generic;
using System.Data;

namespace Coating
{
    public interface ICommandExecutor
    {
        void ExecuteWriteCommand(SqlCommand sqlCommand);
        IEnumerable<IDataRecord> ExecuteReadCommand(SqlCommand sqlCommand);
    }
}