using System.Collections.Generic;
using System.Linq;

namespace Coating.Tests.TestDoubles
{
    public class StubDatabaseFacade : IDatabaseFacade
    {
        private readonly DataDocument _selectByIdResult;
        private readonly IEnumerable<DataDocument> _selectByTypeResult;

        public StubDatabaseFacade(DataDocument selectByIdResult = null, IEnumerable<DataDocument> selectByTypeResult = null)
        {
            _selectByIdResult = selectByIdResult;
            _selectByTypeResult = selectByTypeResult;
        }

        public void Insert(DataDocument document)
        {
            throw new System.NotImplementedException();
        }

        public void Update(DataDocument document)
        {
            throw new System.NotImplementedException();
        }

        public DataDocument SelectById(string id)
        {
            return _selectByIdResult;
        }

        public IEnumerable<DataDocument> SelectByType(string typeName)
        {
            if (_selectByTypeResult == null)
            {
                return Enumerable.Empty<DataDocument>();
            }

            return _selectByTypeResult;
        }
    }
}