using System.Collections.Generic;

namespace Coating.Tests.TestDoubles
{
    public abstract class AbstractStorageFacade : IStorageFacade
    {
        public virtual void Insert(DataDocument document)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Update(DataDocument document)
        {
            throw new System.NotImplementedException();
        }

        public virtual DataDocument SelectById(string id)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<DataDocument> SelectByType(string typeName)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool Contains(string id)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}