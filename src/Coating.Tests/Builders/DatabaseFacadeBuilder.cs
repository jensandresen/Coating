using Moq;

namespace Coating.Tests.Builders
{
    public class DatabaseFacadeBuilder
    {
        private ICommandFactory _commandFactory;
        private ICommandExecutor _commandExecutor;

        public DatabaseFacadeBuilder()
        {
            _commandFactory = new Mock<ICommandFactory>().Object;
            _commandExecutor = new Mock<ICommandExecutor>().Object;
        }

        public DatabaseFacadeBuilder WithCommandFactory(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            return this;
        }

        public DatabaseFacadeBuilder WithExecutor(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
            return this;
        }

        public DatabaseFacade Build()
        {
            return new DatabaseFacade(_commandFactory, _commandExecutor);
        }
    }
}