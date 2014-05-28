using System;
using System.Collections.Generic;
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
                dbCommand.Connection = _connection;
                dbCommand.ExecuteNonQuery();
            }
        }

        public virtual IEnumerable<IDataRecord> ExecuteReadCommand(SqlCommand sqlCommand)
        {
            using (var dbCommand = _mapper.Map(sqlCommand))
            {
                using (var reader = dbCommand.ExecuteReader())
                {
                    while (reader != null && reader.Read())
                    {
                        yield return reader;
                    }
                }
            }
        }
    }
}