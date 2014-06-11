using Moq;

namespace Coating.Tests.Builders
{
    public class DocumentsBuilder
    {
        private IStorageFacade _storageFacade;
        private IIdService _idService;
        private ITypeService _typeService;
        private ISerializationService _serializationService;

        public DocumentsBuilder()
        {
            _storageFacade = new Mock<IStorageFacade>().Object;
            _idService = new Mock<IIdService>().Object;
            _typeService = new Mock<ITypeService>().Object;
            _serializationService = new Mock<ISerializationService>().Object;
        }

        public DocumentsBuilder WithStorageFacade(IStorageFacade storageFacade)
        {
            _storageFacade = storageFacade;
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
            return new Documents(_storageFacade, _idService, _typeService, _serializationService);
        }
    }
}