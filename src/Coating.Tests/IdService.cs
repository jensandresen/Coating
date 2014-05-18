namespace Coating.Tests
{
    public class IdService : IIdService
    {
        private readonly string _propertyName;

        public IdService() : this("Id")
        {
            
        }

        public IdService(string propertyName)
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

            var idProperty = typeof(T).GetProperty(_propertyName);
            var value = idProperty.GetValue(o, null);

            if (value == null)
            {
                return null;
            }

            return value.ToString();
        }
    }
}