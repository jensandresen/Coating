namespace Coating.Tests.TestDoubles
{
    public class SpyDatabaseFacade : IDatabaseFacade
    {
        public DataDocument insertedDocument;

        public void Insert(DataDocument document)
        {
            insertedDocument = document;
        }
    }
}