using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestJsonSerializationService
    {
        [Test]
        public void returns_expected_when_serializing_null()
        {
            var sut = new JsonSerializationService();
            var actual = sut.Serialize(null);

            Assert.AreEqual("", actual);
        }

        [Test]
        public void returns_expected_when_serializing_simple_object()
        {
            var sut = new JsonSerializationService();
            var actual = sut.Serialize(new
                {
                    Foo = "1",
                    Bar = "2"
                });

            var expected = "{\"Foo\":\"1\",\"Bar\":\"2\"}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void returns_expected_when_deserializing_simple_object()
        {
            var sut = new JsonSerializationService();
            var actual = sut.Deserialize<FooBar>("{\"Foo\":\"1\",\"Bar\":\"2\"}");
            var expected = new FooBar
                {
                    Foo = "1",
                    Bar = "2"
                };

            Assert.AreEqual(expected.Foo, actual.Foo);
            Assert.AreEqual(expected.Bar, actual.Bar);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("invalid-json")]
        public void returns_expected_when_deserializing_invalid_json(string jsonData)
        {
            var sut = new JsonSerializationService();
            var actual = sut.Deserialize<FooBar>(jsonData);

            Assert.IsNull(actual);
        }

        private class FooBar
        {
            public string Foo { get; set; }
            public string Bar { get; set; }

            public override string ToString()
            {
                return string.Format("Foo: {0}, Bar: {1}", Foo, Bar);
            }
        }
    }
}