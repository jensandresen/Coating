using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestSqlCommand
    {
        [Test]
        public void returns_expected_when_comparing_two_equal_commands_without_parameters()
        {
            var c1 = new SqlCommand {Sql = "dummy sql string"};
            var c2 = new SqlCommand {Sql = "dummy sql string"};

            Assert.AreEqual(c1, c2);
        }

        [Test]
        public void returns_expected_when_comparing_two_non_equal_commands_without_parameters()
        {
            var c1 = new SqlCommand {Sql = "Foo"};
            var c2 = new SqlCommand {Sql = "Bar"};

            Assert.AreNotEqual(c1, c2);
        }

        [Test]
        public void returns_expected_when_comparing_two_equal_commands_with_equal_parameters()
        {
            var c1 = new SqlCommand { Sql = "dummy sql string" };
            c1.AddParameter("Foo", "Bar");

            var c2 = new SqlCommand { Sql = "dummy sql string" };
            c2.AddParameter("Foo", "Bar");

            Assert.AreEqual(c1, c2);
        }

        [Test]
        public void returns_expected_when_comparing_two_equal_commands_with_non_equal_parameters()
        {
            var c1 = new SqlCommand { Sql = "dummy sql string" };
            c1.AddParameter("Foo", "Bar");

            var c2 = new SqlCommand { Sql = "dummy sql string" };
            c2.AddParameter("Baz", "Qux");

            Assert.AreNotEqual(c1, c2);
        }

        [Test]
        public void creates_expected_ado_insert_command()
        {
            var sut = new CommandFactory();

            var actual = sut
                .CreateInsertCommandFor("1", "the data", "the type")
                .ToAdoCommand();

            var expected = new System.Data.SqlClient.SqlCommand();
            expected.CommandText = "insert into Data(Id, Data, Type) values(@Id, @Data, @Type)";
            expected.CommandType = CommandType.Text;
            expected.Parameters.AddWithValue("@Id", "1");
            expected.Parameters.AddWithValue("@Data", "the data");
            expected.Parameters.AddWithValue("@Type", "the type");

            CompareCommands(expected, actual);
        }

        [Test]
        public void returns_expected_on_to_string()
        {
            var sut = new SqlCommand();
            sut.Sql = "insert into Data(Id, Data, Type) values(@Id, @Data, @Type)";
            sut.AddParameter("@Id", "1");
            sut.AddParameter("@Data", "the data");
            sut.AddParameter("@Type", "the type");

            Assert.AreEqual("insert into Data(Id, Data, Type) values('1', 'the data', 'the type')", sut.ToString());
        }

        private static void CompareCommands(IDbCommand left, IDbCommand right)
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