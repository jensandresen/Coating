using Moq;

namespace Coating.Tests.Builders
{
    public class DocumentsBuilder
    {
        private IDatabaseFacade _databaseFacade;
        private IIdService _idService;
        private ITypeService _typeService;
        private ISerializationService _serializationService;

        public DocumentsBuilder()
        {
            _databaseFacade = new Mock<IDatabaseFacade>().Object;
            _idService = new Mock<IIdService>().Object;
            _typeService = new Mock<ITypeService>().Object;
            _serializationService = new Mock<ISerializationService>().Object;
        }

        public DocumentsBuilder WithDatabaseFacade(IDatabaseFacade databaseFacade)
        {
            _databaseFacade = databaseFacade;
            return this;
        }

        public DocumentsBuilder WithIdService(IIdService idService)
        {
            _idService = idService;
            return this;
        }

        public DocumentsBuilder WithTypeService(ITypeService typeService)
        {
            _typeService = typeService;
            return this;
        }

        public DocumentsBuilder WithSerializationService(ISerializationService serializationService)
        {
            _serializationService = serializationService;
            return this;
        }

        public Documents Build()
        {
            return new Documents(_databaseFacade, _idService, _typeService, _serializationService);
        }
    }
}