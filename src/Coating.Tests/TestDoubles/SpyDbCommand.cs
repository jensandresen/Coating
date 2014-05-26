using System.Data;

namespace Coating.Tests.TestDoubles
{
    public class SpyDbCommand : IDbCommand
    {
        public IDbConnection assignedConnection;

        public void Dispose()
        {

        }

        public void Prepare()
        {
            throw new System.NotImplementedException();
        }

        public void Cancel()
        {
            throw new System.NotImplementedException();
        }

        public IDbDataParameter CreateParameter()
        {
            throw new System.NotImplementedException();
        }

        public int ExecuteNonQuery()
        {
            return 0;
        }

        public IDataReader ExecuteReader()
        {
            throw new System.NotImplementedException();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new System.NotImplementedException();
        }

        public object ExecuteScalar()
        {
            throw new System.NotImplementedException();
        }

        public IDbConnection Connection
        {
            get { return assignedConnection; }
            set { assignedConnection = value; }
        }

        public IDbTransaction Transaction { get; set; }
        public string CommandText { get; set; }
        public int CommandTimeout { get; set; }
        public CommandType CommandType { get; set; }
        public IDataParameterCollection Parameters { get; private set; }
        public UpdateRowSource UpdatedRowSource { get; set; }
    }
}