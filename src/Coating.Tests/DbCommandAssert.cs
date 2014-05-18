using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;

namespace Coating.Tests
{
    public static class DbCommandAssert
    {
        public static void AreEqual(IDbCommand left, IDbCommand right)
        {
            Assert.AreEqual(left.CommandText, right.CommandText, "CommandText are not equal");
            Assert.AreEqual(left.CommandType, right.CommandType, "CommandType are not equal");

            var leftParameters = left
                .Parameters
                .Cast<SqlParameter>()
                .Select(x => new { Name = x.ParameterName, Value = x.Value })
                .ToArray();

            var rightParameters = right
                .Parameters
                .Cast<SqlParameter>()
                .Select(x => new { Name = x.ParameterName, Value = x.Value })
                .ToArray();

            CollectionAssert.AreEquivalent(leftParameters, rightParameters);
        }
    }
}