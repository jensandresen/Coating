using System;

namespace Coating
{
    public class CompositeIdService : IIdService
    {
        private readonly IIdService _primaryService;
        private readonly IIdService _secondaryService;

        public CompositeIdService(IIdService primaryService, IIdService secondaryService)
        {
            if (primaryService == null)
            {
                throw new ArgumentNullException("primaryService");
            }

            if (secondaryService == null)
            {
                throw new ArgumentNullException("secondaryService");
            }

            _primaryService = primaryService;
            _secondaryService = secondaryService;
        }

        public IIdService PrimaryService
        {
            get { return _primaryService; }
        }

        public IIdService SecondaryService
        {
            get { return _secondaryService; }
        }

        public string GetIdFrom<T>(T o) where T : class
        {
            var value = _primaryService.GetIdFrom(o);

            if (value == null)
            {
                return _secondaryService.GetIdFrom(o);
            }

            return value;
        }
    }
}