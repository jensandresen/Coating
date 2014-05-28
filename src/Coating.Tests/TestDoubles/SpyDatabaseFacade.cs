using System.Collections.Generic;

namespace Coating.Tests.TestDoubles
{
    public class SpyDatabaseFacade : IDatabaseFacade
    {
        public DataDocument insertedDocument;
        public DataDocument updatedDocument;

        public void Insert(DataDocument document)
        {
            insertedDocument = document;
        }

        public void Update(DataDocument document)
        {
            updatedDocument = document;
        }

        public DataDocument SelectById(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DataDocument> SelectByType(string typeName)
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}