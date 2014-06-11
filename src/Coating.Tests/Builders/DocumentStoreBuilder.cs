using Moq;

namespace Coating.Tests.Builders
{
    public class DocumentStoreBuilder
    {
        private IStorageFacade _storageFacade;
        private IIdService _idService;
        private ITypeService _typeService;
        private ISerializationService _serializationService;

        public DocumentStoreBuilder()
        {
            _storageFacade = new Mock<IStorageFacade>().Object;
            _idService = new Mock<IIdService>().Object;
            _typeService = new Mock<ITypeService>().Object;
            _serializationService = new Mock<ISerializationService>().Object;
        }

        public DocumentStoreBuilder WithStorageFacade(IStorageFacade storageFacade)
        {
            _storageFacade = storageFacade;
            return this;
        }

        public DocumentStoreBuilder WithIdService(IIdService idService)
        {
            _idService = idService;
            return this;
        }

        public DocumentStoreBuilder WithTypeService(ITypeService typeService)
        {
            _typeService = typeService;
            return this;
        }

        public DocumentStoreBuilder WithSerializationService(ISerializationService serializationService)
        {
            _serializationService = serializationService;
            return this;
        }

        public DocumentStore Build()
        {
            return new DocumentStore(_storageFacade, _idService, _typeService, _serializationService);
        }
    }
}