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

            var expected = "{ \"Foo\": \"1\", \"Bar\": \"2\" }";

            Assert.AreEqual(expected, actual);
        }
    }
}