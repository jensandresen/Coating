using System.Data;

namespace Coating.Tests
{
    public interface ICommandMapper
    {
        IDbCommand Map(SqlCommand sqlCommand);
    }
}