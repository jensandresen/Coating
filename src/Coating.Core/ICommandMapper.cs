using System.Data;

namespace Coating
{
    public interface ICommandMapper
    {
        IDbCommand Map(SqlCommand sqlCommand);
    }
}