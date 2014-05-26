namespace Coating
{
    public class Documents
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
    }
}