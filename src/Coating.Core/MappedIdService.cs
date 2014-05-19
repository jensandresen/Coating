using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Coating
{
    public class MappedIdService : IIdService
    {
        private readonly Dictionary<Type, IIdMap> _maps = new Dictionary<Type, IIdMap>();

        public IEnumerable<IIdMap> Maps
        {
            get { return _maps.Values; }
        }

        public string GetIdFrom<T>(T o) where T : class
        {
            IIdMap map;

            if (!_maps.TryGetValue(typeof (T), out map))
            {
                return null;
            }

            var value = map.GetValue(o);

            if (value == null)
            {
                return null;
            }

            return value.ToString();
        }

        public string GetIdFrom<T>(T o, Expression<Func<T, object>> idProperty) where T : class
        {
            if (o == null)
            {
                return null;
            }

            var idValueGetter = idProperty.Compile();
            var idMap = new IdMap<T>(idValueGetter);

            var value = idMap.GetValue(o);

            if (value == null)
            {
                return null;
            }

            return value.ToString();
        }

        public void Map<T>(Expression<Func<T, object>> idProperty) where T : class
        {
            var idValueGetter = idProperty.Compile();
            IIdMap idMap = new IdMap<T>(idValueGetter);

            if (_maps.ContainsKey(idMap.SupportedType))
            {
                throw new Exception(string.Format("Map for {0} already registered.", idMap.SupportedType.Name));
            }

            _maps.Add(idMap.SupportedType, idMap);
        }
    }
}