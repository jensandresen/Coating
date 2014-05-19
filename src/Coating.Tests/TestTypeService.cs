using Coating.Core;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestTypeService
    {
        [Test]
        public void returns_expected_type_from_foo_class()
        {
            var sut = new TypeService();
            var actual = sut.GetTypeNameFrom(new Foo());

            Assert.AreEqual("Foo", actual);
        }

        [Test]
        public void returns_expected_type_from_bar_class()
        {
            var sut = new TypeService();
            var actual = sut.GetTypeNameFrom(new Bar());

            Assert.AreEqual("Bar", actual);
        }

        private class Foo { }
        private class Bar { }
    }
}