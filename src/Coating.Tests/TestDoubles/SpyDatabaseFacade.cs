namespace Coating.Tests.TestDoubles
{
    public class SpyDatabaseFacade : AbstractDatabaseFacade
    {
        public DataDocument insertedDocument;
        public DataDocument updatedDocument;

        public override void Insert(DataDocument document)
        {
            insertedDocument = document;
        }

        public override void Update(DataDocument document)
        {
            updatedDocument = document;
        }
    }
}