using System.Collections.Generic;

namespace Coating
{
    public interface IDocuments
    {
        void Store(object document);
        T Retrieve<T>(string id) where T : class, new();
        IEnumerable<T> RetrieveAll<T>() where T : class, new();
    }
}