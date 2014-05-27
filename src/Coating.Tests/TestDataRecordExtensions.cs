using System.Data;
using Coating.Tests.TestDoubles;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestDataRecordExtensions
    {
        [TestCase("Foo")]
        [TestCase("Bar")]
        [TestCase("Baz")]
        [TestCase("Qux")]
        public void can_retrive_a_string(object expected)
        {
            var stubDataRecord = new StubDataRecord(expected);
            var actual = stubDataRecord.GetString("dummy fieldName");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void returns_null_if_data_record_is_null()
        {
            IDataRecord nullRecord = null;
            var actual = nullRecord.GetString("dummy fieldName");

            Assert.IsNull(actual);
        }
    }
}