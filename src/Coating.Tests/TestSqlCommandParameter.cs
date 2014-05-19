using Coating.Core;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestSqlCommandParameter
    {
        [TestCase("Foo", "Bar")]
        [TestCase("Foo", 1)]
        [TestCase("Foo", 2d)]
        [TestCase("Foo", 3f)]
        [TestCase("Foo", true)]
        [TestCase("Foo", false)]
        public void returns_expected_when_comparing_two_equal_parameters(string name, object value)
        {
            var p1 = new SqlCommandParameter {Name = name, Value = value};
            var p2 = new SqlCommandParameter {Name = name, Value = value};

            Assert.AreEqual(p1, p2);
        }

        [Test]
        public void returns_expected_when_comparing_two_non_equal_parameters()
        {
            var p1 = new SqlCommandParameter {Name = "Foo", Value = "Bar"};
            var p2 = new SqlCommandParameter {Name = "Baz", Value = "Qux"};

            Assert.AreNotEqual(p1, p2);
        }
    }
}