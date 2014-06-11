using Moq;

namespace Coating.Tests.Builders
{
    public class StorageFacadeBuilder
    {
        private ICommandFactory _commandFactory;
        private ICommandExecutor _commandExecutor;

        public StorageFacadeBuilder()
        {
            _commandFactory = new Mock<ICommandFactory>().Object;
            _commandExecutor = new Mock<ICommandExecutor>().Object;
        }

        public StorageFacadeBuilder WithCommandFactory(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            return this;
        }

        public StorageFacadeBuilder WithExecutor(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
            return this;
        }

        public StorageFacade Build()
        {
            return new StorageFacade(_commandFactory, _commandExecutor);
        }
    }
}