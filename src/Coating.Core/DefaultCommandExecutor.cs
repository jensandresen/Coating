using System;
using System.Data;

namespace Coating
{
    public class DefaultCommandExecutor : ICommandExecutor
    {
        private readonly IDbConnection _connection;
        private readonly ICommandMapper _mapper;

        public DefaultCommandExecutor(IDbConnection connection, ICommandMapper mapper)
        {
            _connection = connection;
            _mapper = mapper;
        }

        protected IDbConnection Connection
        {
            get { return _connection; }
        }

        public virtual void ExecuteWriteCommand(SqlCommand sqlCommand)
        {
            using (var dbCommand = _mapper.Map(sqlCommand))
            {
                ExecuteWriteCommand(dbCommand);
            }
        }

        protected virtual void ExecuteWriteCommand(IDbCommand dbCommand)
        {
            dbCommand.Connection = Connection;
            dbCommand.ExecuteNonQuery();
        }

        public virtual void ExecuteReadCommand(SqlCommand sqlCommand, Action<IDataRecord> callback)
        {
            using (var dbCommand = _mapper.Map(sqlCommand))
            {
                dbCommand.Connection = _connection;

                using (var reader = dbCommand.ExecuteReader())
                {
                    while (reader != null && reader.Read())
                    {
                        callback(reader);
                    }
                }
            }
        }
    }
}