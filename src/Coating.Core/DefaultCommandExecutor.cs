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

        public void ExecuteWriteCommand(SqlCommand sqlCommand)
        {
            using (var dbCommand = _mapper.Map(sqlCommand))
            {
                dbCommand.Connection = _connection;
                dbCommand.ExecuteNonQuery();
            }
        }
    }
}