using System.Collections.Generic;
using System.Linq;

namespace Coating.Tests.TestDoubles
{
    public class StubDatabaseFacade : AbstractDatabaseFacade
    {
        private readonly DataDocument _selectByIdResult;
        private readonly IEnumerable<DataDocument> _selectByTypeResult;

        public StubDatabaseFacade(DataDocument selectByIdResult = null, IEnumerable<DataDocument> selectByTypeResult = null)
        {
            _selectByIdResult = selectByIdResult;
            _selectByTypeResult = selectByTypeResult;
        }

        public override DataDocument SelectById(string id)
        {
            return _selectByIdResult;
        }

        public override IEnumerable<DataDocument> SelectByType(string typeName)
        {
            if (_selectByTypeResult == null)
            {
                return Enumerable.Empty<DataDocument>();
            }

            return _selectByTypeResult;
        }
    }
}