using System.Collections.Generic;
using System.Linq;

namespace Coating.Tests.TestDoubles
{
    public class StubDatabaseFacade : AbstractDatabaseFacade
    {
        private readonly DataDocument _selectByIdResult;
        private readonly IEnumerable<DataDocument> _selectByTypeResult;
        private readonly bool _containsResult;

        public StubDatabaseFacade(DataDocument selectByIdResult = null, IEnumerable<DataDocument> selectByTypeResult = null, bool containsResult = true)
        {
            _selectByIdResult = selectByIdResult;
            _selectByTypeResult = selectByTypeResult;
            _containsResult = containsResult;
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

        public override bool Contains(string id)
        {
            return _containsResult;
        }
    }
}