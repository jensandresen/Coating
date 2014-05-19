using System;

namespace Coating
{
    public interface IIdMap
    {
        object GetValue(object o);
        Type SupportedType { get; }
    }
}