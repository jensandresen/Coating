using System.Data;

namespace Coating
{
    public class TransactionalCommandExecutor : DefaultCommandExecutor
    {
        public TransactionalCommandExecutor(IDbConnection connection, ICommandMapper mapper) : base(connection, mapper)
        {

        }

        protected override void ExecuteWriteCommand(IDbCommand dbCommand)
        {
            using (var transaction = Connection.BeginTransaction())
            {
                try
                {
                    dbCommand.Connection = Connection;
                    dbCommand.Transaction = transaction;
                    dbCommand.ExecuteNonQuery();

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