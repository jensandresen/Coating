using System;
using System.Data;

namespace Coating.Tests.TestDoubles
{
    public class StubCommandExecutor : ICommandExecutor
    {
        private readonly IDataRecord _readResult;

        public StubCommandExecutor(IDataRecord readResult = null)
        {
            _readResult = readResult;
        }

        public void ExecuteWriteCommand(SqlCommand sqlCommand)
        {

        }

        public void ExecuteReadCommand(SqlCommand sqlCommand, Action<IDataRecord> callback)
        {
            callback(_readResult);
        }
    }
}