using System;

namespace Coating.Core
{
    public class IdMap<T> : IIdMap where T : class
    {
        private readonly Func<T, object> _getter;

        public IdMap(Func<T, object> getter)
        {
            _getter = getter;
        }

        public object GetValue(T o)
        {
            return _getter(o);
        }

        public object GetValue(object o)
        {
            return GetValue(o as T);
        }

        public Type SupportedType
        {
            get { return typeof (T); }
        }
    }
}