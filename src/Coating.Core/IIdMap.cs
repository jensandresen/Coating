using System;

namespace Coating.Core
{
    public interface IIdMap
    {
        object GetValue(object o);
        Type SupportedType { get; }
    }
}