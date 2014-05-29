using System.Data;

namespace Coating
{
    public class DefaultCommandMapper : ICommandMapper
    {
        public IDbCommand Map(SqlCommand sqlCommand)
        {
            return sqlCommand.ToAdoCommand();
        }
    }
}