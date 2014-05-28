using System.Data;

namespace Coating
{
    public class TransactionalCommandExecutor : DefaultCommandExecutor
    {
        public TransactionalCommandExecutor(IDbConnection connection, ICommandMapper mapper) : base(connection, mapper)
        {

        }

        public override void ExecuteWriteCommand(SqlCommand sqlCommand)
        {
            using (var transaction = Connection.BeginTransaction())
            {
                try
                {
                    base.ExecuteWriteCommand(sqlCommand);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}