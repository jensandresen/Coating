using System.Collections.Generic;

namespace Coating
{
    public interface IStorageFacade
    {
        void Insert(DataDocument document);
        void Update(DataDocument document);
        DataDocument SelectById(string id);
        IEnumerable<DataDocument> SelectByType(string typeName);
        bool Contains(string id);
        void Delete(string id);
    }
}