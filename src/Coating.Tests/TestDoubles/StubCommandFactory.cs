namespace Coating.Tests.TestDoubles
{
    public class StubCommandFactory : ICommandFactory
    {
        private readonly SqlCommand _insert;
        private readonly SqlCommand _update;
        private readonly SqlCommand _delete;
        private readonly SqlCommand _selectById;
        private readonly SqlCommand _selectByType;

        public StubCommandFactory(SqlCommand insert = null, 
                                  SqlCommand update = null,
                                  SqlCommand delete = null,
                                  SqlCommand selectById = null,
                                  SqlCommand selectByType = null)
        {
            _insert = insert;
            _update = update;
            _delete = delete;
            _selectById = selectById;
            _selectByType = selectByType;
        }

        public SqlCommand CreateInsertCommandFor(string id, string data, string typeName)
        {
            return _insert;
        }

        public SqlCommand CreateUpdateCommandFor(string id, string theNewData, string typeName)
        {
            return _update;
        }

        public SqlCommand CreateDeleteCommandFor(string id)
        {
            return _delete;
        }

        public SqlCommand CreateSelectByIdCommandFor(string id)
        {
            return _selectById;
        }

        public SqlCommand CreateSelectByTypeCommandFor(string typeName)
        {
            return _selectByType;
        }
    }
}