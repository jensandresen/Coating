using System.Collections.Generic;

namespace Coating
{
    public class DocumentStore : IDocumentStore
    {
        private readonly IStorageFacade _storageFacade;
        private readonly IIdService _idService;
        private readonly ITypeService _typeService;
        private readonly ISerializationService _serializationService;

        public DocumentStore(IStorageFacade storageFacade, IIdService idService, ITypeService typeService, ISerializationService serializationService)
        {
            _storageFacade = storageFacade;
            _idService = idService;
            _typeService = typeService;
            _serializationService = serializationService;
        }

        public IStorageFacade StorageFacade
        {
            get { return _storageFacade; }
        }

        public IIdService IdService
        {
            get { return _idService; }
        }

        public ITypeService TypeService
        {
            get { return _typeService; }
        }

        public ISerializationService SerializationService
        {
            get { return _serializationService; }
        }

        public void Save(object document)
        {
            var documentId = _idService.GetIdFrom(document);
            var documentType = _typeService.GetTypeNameFrom(document);
            var id = string.Format("{0}/{1}", documentType, documentId);

            var data = _serializationService.Serialize(document);
            _storageFacade.Insert(new DataDocument
                {
                    Id = id,
                    Type = documentType,
                    Data = data
                });
        }

        public void Update(object document)
        {
            var documentId = _idService.GetIdFrom(document);
            var documentType = _typeService.GetTypeNameFrom(document);
            var id = string.Format("{0}/{1}", documentType, documentId);

            var data = _serializationService.Serialize(document);
            _storageFacade.Update(new DataDocument
                {
                    Id = id,
                    Type = documentType,
                    Data = data
                });
        }

        public bool Contains<T>(T document) where T : class 
        {
            var documentId = _idService.GetIdFrom(document);
            var documentType = _typeService.GetTypeNameFrom(document);
            var id = string.Format("{0}/{1}", documentType, documentId);

            return _storageFacade.Contains(id);
        }

        public void Store<T>(T document) where T : class
        {
            var alreadyExists = Contains(document);

            if (alreadyExists)
            {
                Update(document);
            }
            else
            {
                Save(document);
            }
        }

        public T Retrieve<T>(string id) where T : class, new()
        {
            var documentType = _typeService.GetTypeNameFrom<T>();
            var dataDocumentId = string.Format("{0}/{1}", documentType, id);

            var dataDocument = _storageFacade.SelectById(dataDocumentId);  

            if (dataDocument == null)
            {
                return null;
            }

            return _serializationService.Deserialize<T>(dataDocument.Data);
        }

        public IEnumerable<T> RetrieveAll<T>() where T : class, new()
        {
            var typeName = _typeService.GetTypeNameFrom<T>();
            var dataDocuments = _storageFacade.SelectByType(typeName);

            foreach (var document in dataDocuments) 
            {
                yield return _serializationService.Deserialize<T>(document.Data);
            }
        }

        public void Delete<T>(string id) where T : class
        {
            var documentType = _typeService.GetTypeNameFrom<T>();
            var documentId = string.Format("{0}/{1}", documentType, id);

            _storageFacade.Delete(documentId);
        }
    }
}