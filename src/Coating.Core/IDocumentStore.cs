using System.Collections.Generic;

namespace Coating
{
    public interface IDocumentStore
    {
        void Store<T>(T document) where T : class;
        T Retrieve<T>(string id) where T : class, new();
        IEnumerable<T> RetrieveAll<T>() where T : class, new();
        void Delete<T>(string id) where T : class;
    }
}