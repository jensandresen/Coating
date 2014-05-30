using System.Data;

namespace Coating.Tests.TestDoubles
{
    public class StubDbConnection : IDbConnection
    {
        private readonly IDbTransaction _resultTransaction;

        public StubDbConnection(IDbTransaction resultTransaction = null)
        {
            _resultTransaction = resultTransaction;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IDbTransaction BeginTransaction()
        {
            return _resultTransaction;
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return _resultTransaction;
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new System.NotImplementedException();
        }

        public IDbCommand CreateCommand()
        {
            throw new System.NotImplementedException();
        }

        public void Open()
        {
            throw new System.NotImplementedException();
        }

        public string ConnectionString { get; set; }
        public int ConnectionTimeout { get; private set; }
        public string Database { get; private set; }
        public ConnectionState State { get; private set; }
    }
}