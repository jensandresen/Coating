using System;
using System.Linq;
using System.Reflection;

namespace Coating
{
    public class AutoIdService : IIdService
    {
        private readonly string _propertyName;

        public AutoIdService() : this("Id")
        {
            
        }

        public AutoIdService(string propertyName)
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