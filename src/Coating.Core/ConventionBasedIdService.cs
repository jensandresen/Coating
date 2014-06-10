using System;

namespace Coating
{
    public class ConventionBasedIdService : IIdService
    {
        private readonly Func<Type, string> _nameConvention;

        public ConventionBasedIdService() : this("Id")
        {
            
        }

        public ConventionBasedIdService(string propertyName) : this(type => propertyName)
        {
        }

        public ConventionBasedIdService(Func<Type, string> nameConvention)
        {
            _nameConvention = nameConvention;
        }

        public Func<Type, string> NameConvention
        {
            get { return _nameConvention; }
        }

        public string GetIdFrom<T>(T o) where T : class
        {
            if (o == null)
            {
                return null;
            }

            var type = o.GetType();
            var propertyName = _nameConvention(type);

            var idProperty = type.GetProperty(propertyName);
            var value = idProperty.GetValue(o, null);

            if (value == null)
            {
                return null;
            }

            return value.ToString();
        }
    }
}