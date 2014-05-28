using System;

namespace Coating
{
    public class TypeService : ITypeService
    {
        private string GetTypeNameFrom(Type type)
        {
            return type.Name;
        }

        public string GetTypeNameFrom(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            return GetTypeNameFrom(instance.GetType());
        }

        public string GetTypeNameFrom<T>()
        {
            return GetTypeNameFrom(typeof (T));
        }
    }
}