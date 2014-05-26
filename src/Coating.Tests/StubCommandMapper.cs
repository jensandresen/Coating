using System.Data;

namespace Coating.Tests
{
    public class StubCommandMapper : ICommandMapper
    {
        private readonly IDbCommand _dbCommand;

        public StubCommandMapper(IDbCommand dbCommand)
        {
            _dbCommand = dbCommand;
        }

        public IDbCommand Map(SqlCommand sqlCommand)
        {
            return _dbCommand;
        }
    }
}