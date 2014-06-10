using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestConventionBasedIdService
    {
        [Test]
        public void returns_expected_default_id_property_name()
        {
            var sut = new ConventionBasedIdService();
            Assert.AreEqual("Id", sut.PropertyName);
        }

        [TestCase("Id")]
        [TestCase("Foo")]
        [TestCase("Bar")]
        [TestCase("Baz")]
        [TestCase("Qux")]
        public void returns_expected_id_property_name_when_set(string expectedPropertyName)
        {
            var sut = new ConventionBasedIdService(expectedPropertyName);
            Assert.AreEqual(expectedPropertyName, sut.PropertyName);
        }

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
            var sut = new ConventionBasedIdService();
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

            var sut = new ConventionBasedIdService("Key");
            var actual = sut.GetIdFrom(foo);

            Assert.AreEqual(foo.Key, actual);
        }

        [Test]
        public void returns_null_if_value_of_id_property_is_null()
        {
            var sut = new ConventionBasedIdService();
            var actual = sut.GetIdFrom(new Foo
                {
                    Id = null,
                    Key = "1"
                });

            Assert.IsNull(actual);
        }

        [Test]
        public void returns_null_if_trying_to_get_an_id_from_null()
        {
            var sut = new ConventionBasedIdService();
            var actual = sut.GetIdFrom<Foo>(null);

            Assert.IsNull(actual);
        }

        [Test]
        public void is_instance_of_common_abstraction()
        {
            var sut = new ConventionBasedIdService();
            Assert.IsInstanceOf<IIdService>(sut);
        }

        private class Foo
        {
            public string Id { get; set; }
            public string Key { get; set; }
        }
    }
}