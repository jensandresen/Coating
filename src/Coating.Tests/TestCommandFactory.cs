using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestCommandFactory
    {
        [Test]
        public void creates_expected_insert_command()
        {
            var sut = new CommandFactory();

            var actual = sut.CreateInsertCommandFor("1", "the data", "the type");

            var expected = new SqlCommand();
            expected.Sql = "insert into Data(Id, Data, Type) values(@Id, @Data, @Type)";
            expected.AddParameter("@Id", "1");
            expected.AddParameter("@Data", "the data");
            expected.AddParameter("@Type", "the type");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void creates_expected_update_command()
        {
            var sut = new CommandFactory();

            var actual = sut.CreateUpdateCommandFor("1", "the new data", "the type");

            var expected = new SqlCommand();
            expected.Sql = "update Data set Data = @Data, Type = @Type where Id = @Id";
            expected.AddParameter("@Id", "1");
            expected.AddParameter("@Data", "the new data");
            expected.AddParameter("@Type", "the type");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void creates_expected_delete_command()
        {
            var sut = new CommandFactory();

            var actual = sut.CreateDeleteCommandFor("1");

            var expected = new SqlCommand();
            expected.Sql = "delete from Data where Id = @Id";
            expected.AddParameter("@Id", "1");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void creates_expected_select_by_id_command()
        {
            var sut = new CommandFactory();

            var actual = sut.CreateSelectByIdCommandFor("1");

            var expected = new SqlCommand();
            expected.Sql = "select * from Data where Id = @Id";
            expected.AddParameter("@Id", "1");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void creates_expected_select_by_type_command()
        {
            var sut = new CommandFactory();

            var actual = sut.CreateSelectByTypeCommandFor("Foo");

            var expected = new SqlCommand();
            expected.Sql = "select * from Data where Type = @Type";
            expected.AddParameter("@Type", "Foo");

            Assert.AreEqual(expected, actual);
        }
    }
}