using System;
using System.Linq;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestMappedIdService
    {
        [Test]
        public void returns_null_if_no_maps_are_added()
        {
            var sut = new MappedIdService();
            var actual = sut.GetIdFrom(new Foo
                {
                    Id = "1",
                    Key = "2"
                });

            Assert.IsNull(actual);
        }

        [Test]
        public void maps_are_empty_by_default()
        {
            var sut = new MappedIdService();
            CollectionAssert.IsEmpty(sut.Maps);
        }

        [Test]
        public void can_get_value_of_explictily_mapped_property()
        {
            var sut = new MappedIdService();

            var actual = sut.GetIdFrom(new Foo
                {
                    Id = "1",
                    Key = "2"
                }, x => x.Key);

            Assert.AreEqual("2", actual);
        }

        [Test]
        public void can_add_map()
        {
            var sut = new MappedIdService();
            sut.Map<Foo>(x => x.Key);

            Assert.AreEqual(1, sut.Maps.Count());
        }

        [Test]
        public void uses_added_map()
        {
            var sut = new MappedIdService();
            sut.Map<Foo>(x => x.Key);

            var actual = sut.GetIdFrom(new Foo
            {
                Id = "1",
                Key = "2"
            });

            Assert.AreEqual("2", actual);
        }

        [Test]
        public void returns_null_when_trying_to_map_null()
        {
            var sut = new MappedIdService();
            var actual = sut.GetIdFrom<Foo>(null);

            Assert.IsNull(actual);
        }

        [Test]
        public void returns_null_when_trying_to_map_null_with_explicit_map()
        {
            var sut = new MappedIdService();
            var actual = sut.GetIdFrom<Foo>(null, x => x.Key);

            Assert.IsNull(actual);
        }

        [Test]
        public void returns_null_when_value_of_id_property_is_null()
        {
            var sut = new MappedIdService();
            sut.Map<Foo>(x => x.Key);

            var actual = sut.GetIdFrom(new Foo
                {
                    Id = "1",
                    Key = null
                });

            Assert.IsNull(actual);
        }

        [Test]
        public void returns_null_when_value_of_id_property_is_null_and_mapping_with_explicit_map()
        {
            var sut = new MappedIdService();

            var actual = sut.GetIdFrom(new Foo
                {
                    Id = "1",
                    Key = null
                }, x => x.Key);

            Assert.IsNull(actual);
        }

        [Test]
        public void throws_exception_when_adding_map_for_same_type_more_than_once()
        {
            var sut = new MappedIdService();
            sut.Map<Foo>(x => x.Key);
            Assert.Throws<Exception>(() => sut.Map<Foo>(x => x.Key));
        }

        private class Foo
        {
            public string Id { get; set; }
            public string Key { get; set; }
        }
    }
}