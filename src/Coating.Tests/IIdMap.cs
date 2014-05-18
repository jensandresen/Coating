using System;

namespace Coating.Tests
{
    public interface IIdMap
    {
        object GetValue(object o);
        Type SupportedType { get; }
    }
}