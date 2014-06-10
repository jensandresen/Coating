using System.Data;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestDefaultCommandMapper
    {
        [Test]
        public void is_a_commandmapper()
        {
            var sut = new DefaultCommandMapper();
            Assert.IsInstanceOf<ICommandMapper>(sut);
        }

        [Test]
        public void maps_expected_command()
        {
            var stubSqlCommand = new SqlCommand();
            stubSqlCommand.Sql = "dummy sql";
            stubSqlCommand.AddParameter("Foo", 1);
            stubSqlCommand.AddParameter("Bar", 2);

            using (var expected = new System.Data.SqlClient.SqlCommand())
            {
                expected.CommandText = "dummy sql";
                expected.CommandType = CommandType.Text;
                expected.Parameters.AddWithValue("Foo", 1);
                expected.Parameters.AddWithValue("Bar", 2);

                var sut = new DefaultCommandMapper();
                var actual = sut.Map(stubSqlCommand);

                DbCommandAssert.AreEqual(expected, actual);
            }
        }
    }
}