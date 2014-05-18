using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestIdService
    {
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("Foo")]
        [TestCase("Bar")]
        [TestCase("Baz")]
        [TestCase("Qux")]
        [TestCase("with spaces")]
        public void returns_value_of_id_property_by_default(string expected)
        {
            var sut = new IdService();
            var actual = sut.GetIdFrom(new Foo {Id = expected});

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void can_use_alternative_property_identifier()
        {
            var foo = new Foo
                {
                    Id = "Foo",
                    Key = "Bar"
                };

            var sut = new IdService("Key");
            var actual = sut.GetIdFrom(foo);

            Assert.AreEqual(foo.Key, actual);
        }

        private class Foo
        {
            public string Id { get; set; }
            public string Key { get; set; }
        }
    }
}