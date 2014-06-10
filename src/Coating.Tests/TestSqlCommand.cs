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
        public void returns_expected_on_to_string()
        {
            var sut = new SqlCommand();
            sut.Sql = "insert into Data(Id, Data, Type) values(@Id, @Data, @Type)";
            sut.AddParameter("@Id", "1");
            sut.AddParameter("@Data", "the data");
            sut.AddParameter("@Type", "the type");

            Assert.AreEqual("insert into Data(Id, Data, Type) values('1', 'the data', 'the type')", sut.ToString());
        }

        [Test]
        public void parameters_is_empty_by_default()
        {
            var sut = new SqlCommand();
            CollectionAssert.IsEmpty(sut.Parameters);
        }

        [Test]
        public void parameters_returns_expected_when_adding_single_parameter()
        {
            var stubParameter = new SqlCommandParameter();
            stubParameter.Name = "Foo";
            stubParameter.Value = 1;

            var expected = new[] {stubParameter};

            var sut = new SqlCommand();
            sut.AddParameter("Foo", 1);

            CollectionAssert.AreEquivalent(expected, sut.Parameters);
        }

        [Test]
        public void parameters_returns_expected_when_adding_multiple_parameters()
        {
            var expected = new[]
                {
                    new SqlCommandParameter {Name = "Foo", Value = 1},
                    new SqlCommandParameter {Name = "Bar", Value = 2},
                };

            var sut = new SqlCommand();
            sut.AddParameter("Foo", 1);
            sut.AddParameter("Bar", 2);

            CollectionAssert.AreEquivalent(expected, sut.Parameters);
        }
    }
}