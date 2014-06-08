using System.Collections.Generic;

namespace Coating
{
    public class Documents : IDocuments
    {
        private readonly IDatabaseFacade _databaseFacade;
        private readonly IIdService _idService;
        private readonly ITypeService _typeService;
        private readonly ISerializationService _serializationService;

        public Documents(IDatabaseFacade databaseFacade, IIdService idService, ITypeService typeService, ISerializationService serializationService)
        {
            _databaseFacade = databaseFacade;
            _idService = idService;
            _typeService = typeService;
            _serializationService = serializationService;
        }

        public IDatabaseFacade DatabaseFacade
        {
            get { return _databaseFacade; }
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
            _databaseFacade.Insert(new DataDocument
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
            _databaseFacade.Update(new DataDocument
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

            return _databaseFacade.Contains(id);
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

            var dataDocument = _databaseFacade.SelectById(dataDocumentId);  

            if (dataDocument == null)
            {
                return null;
            }

            return _serializationService.Deserialize<T>(dataDocument.Data);
        }

        public IEnumerable<T> RetrieveAll<T>() where T : class, new()
        {
            var typeName = _typeService.GetTypeNameFrom<T>();
            var dataDocuments = _databaseFacade.SelectByType(typeName);

            foreach (var document in dataDocuments) 
            {
                yield return _serializationService.Deserialize<T>(document.Data);
            }
        }

        public void Delete<T>(string id) where T : class
        {
            var documentType = _typeService.GetTypeNameFrom<T>();
            var documentId = string.Format("{0}/{1}", documentType, id);

            _databaseFacade.Delete(documentId);
        }
    }
}