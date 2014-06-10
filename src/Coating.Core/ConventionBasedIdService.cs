namespace Coating
{
    public class ConventionBasedIdService : IIdService
    {
        private readonly string _propertyName;

        public ConventionBasedIdService() : this("Id")
        {
            
        }

        public ConventionBasedIdService(string propertyName)
        {
            _propertyName = propertyName;
        }

        public string PropertyName
        {
            get { return _propertyName; }
        }

        public string GetIdFrom<T>(T o) where T : class
        {
            if (o == null)
            {
                return null;
            }

            var idProperty = o.GetType().GetProperty(_propertyName);
            var value = idProperty.GetValue(o, null);

            if (value == null)
            {
                return null;
            }

            return value.ToString();
        }
    }
}