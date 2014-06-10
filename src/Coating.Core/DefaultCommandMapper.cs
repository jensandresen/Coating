using System.Data;

namespace Coating
{
    public class DefaultCommandMapper : ICommandMapper
    {
        public IDbCommand Map(SqlCommand sqlCommand)
        {
            var cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sqlCommand.Sql;
            cmd.CommandType = CommandType.Text;

            foreach (var p in sqlCommand.Parameters)
            {
                cmd.Parameters.AddWithValue(p.Name, p.Value);
            }

            return cmd;
        }
    }
}