using System;
using System.Data;

namespace Coating
{
    public interface ICommandExecutor
    {
        void ExecuteWriteCommand(SqlCommand sqlCommand);
        void ExecuteReadCommand(SqlCommand sqlCommand, Action<IDataRecord> callback);
    }
}