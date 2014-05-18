namespace Coating.Tests
{
    public class IdService
    {
        private readonly string _propertyName;

        public IdService() : this("Id")
        {
            
        }

        public IdService(string propertyName)
        {
            _propertyName = propertyName;
        }

        public string GetIdFrom(object o)
        {
            var idProperty = o
                .GetType()
                .GetProperty(_propertyName);

            return idProperty.GetValue(o, null).ToString();
        }
    }
}