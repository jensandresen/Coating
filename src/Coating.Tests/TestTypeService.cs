using System;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestTypeService
    {
        [Test]
        public void returns_expected_type_from_foo_class_instance()
        {
            var sut = new TypeService();
            var actual = sut.GetTypeNameFrom(new Foo());

            Assert.AreEqual("Foo", actual);
        }

        [Test]
        public void returns_expected_type_from_foo_class()
        {
            var sut = new TypeService();
            var actual = sut.GetTypeNameFrom<Foo>();

            Assert.AreEqual("Foo", actual);
        }

        [Test]
        public void returns_expected_type_from_bar_class_instance()
        {
            var sut = new TypeService();
            var actual = sut.GetTypeNameFrom(new Bar());

            Assert.AreEqual("Bar", actual);
        }

        [Test]
        public void returns_expected_type_from_bar_class()
        {
            var sut = new TypeService();
            var actual = sut.GetTypeNameFrom<Bar>();

            Assert.AreEqual("Bar", actual);
        }

        [Test]
        public void throws_exception_if_argument_is_null()
        {
            var sut = new TypeService();
            Assert.Throws<ArgumentNullException>(() => sut.GetTypeNameFrom(null));
        }

        private class Foo { }
        private class Bar { }
    }
}