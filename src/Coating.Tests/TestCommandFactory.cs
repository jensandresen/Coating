using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using System.Linq;

namespace Coating.Tests
{
    [TestFixture]
    public class TestCommandFactory
    {
        [Test]
        public void creates_expected_insert_command()
        {
            var sut = new CommandFactory();

            var actual = sut.CreateInsertCommandFor("Foo/1", "the data", "Foo");

            var expected = new SqlCommand();
            expected.CommandText = "insert into Data(Id, Data, Type) values(@Id, @Data, @Type)";
            expected.CommandType = CommandType.Text;
            expected.Parameters.AddWithValue("@Id", "Foo/1");
            expected.Parameters.AddWithValue("@Data", "the data");
            expected.Parameters.AddWithValue("@Type", "Foo");

            CompareCommands(expected, actual);
        }

        private void CompareCommands(IDbCommand left, IDbCommand right)
        {
            Assert.AreEqual(left.CommandText, right.CommandText, "CommandText are not equal");
            Assert.AreEqual(left.CommandType, right.CommandType, "CommandType are not equal");

            var leftParameters = left
                .Parameters
                .Cast<SqlParameter>()
                .Select(x => new {Name = x.ParameterName, Value = x.Value})
                .ToArray();

            var rightParameters = right
                .Parameters
                .Cast<SqlParameter>()
                .Select(x => new {Name = x.ParameterName, Value = x.Value})
                .ToArray();

            CollectionAssert.AreEquivalent(leftParameters, rightParameters);
        }
    }

    public class CommandFactory
    {
        public IDbCommand CreateInsertCommandFor(string id, string data, string typeName)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "insert into Data(Id, Data, Type) values(@Id, @Data, @Type)";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Data", data);
            cmd.Parameters.AddWithValue("@Type", typeName);

            return cmd;
        }
    }
}