using System;
using Coating.Core;
using Moq;
using NUnit.Framework;

namespace Coating.Tests
{
    [TestFixture]
    public class TestCompositeIdService
    {
        [Test]
        public void is_instance_of_common_idservice_abstraction()
        {
            var sut = new CompositeIdService(new Mock<IIdService>().Object, new Mock<IIdService>().Object);
            Assert.IsInstanceOf<IIdService>(sut);
        }

        [Test]
        public void primary_idservice_returns_expected()
        {
            var primaryIdService = new Mock<IIdService>().Object;
            var secondaryIdService = new Mock<IIdService>().Object;
            
            var sut = new CompositeIdService(primaryIdService, secondaryIdService);

            Assert.AreSame(primaryIdService, sut.PrimaryService);
        }

        [Test]
        public void secondary_idservice_returns_expected()
        {
            var primaryIdService = new Mock<IIdService>().Object;
            var secondaryIdService = new Mock<IIdService>().Object;
            
            var sut = new CompositeIdService(primaryIdService, secondaryIdService);

            Assert.AreSame(secondaryIdService, sut.SecondaryService);
        }

        [Test]
        public void does_not_use_secondary_service_if_primary_gives_a_result()
        {
            var primary = new StubIdService("valid dummy result");
            var secondary = new SpyIdService();

            var sut = new CompositeIdService(primary, secondary);

            sut.GetIdFrom(new object());

            Assert.IsFalse(secondary.wasCalled);
        }

        [Test]
        public void uses_secondary_service_if_primary_does_not_return_a_valid_result()
        {
            var primary = new StubIdService(null);
            var secondary = new SpyIdService();

            var sut = new CompositeIdService(primary, secondary);

            sut.GetIdFrom(new object());

            Assert.IsTrue(secondary.wasCalled);
        }

        [Test]
        public void returns_value_from_primary_if_it_is_valid()
        {
            var primary = new StubIdService("valid dummy value");
            var dummyService = new Mock<IIdService>().Object;

            var sut = new CompositeIdService(primary, dummyService);

            var actual = sut.GetIdFrom(new object());

            Assert.AreEqual("valid dummy value", actual);
        }

        [Test]
        public void returns_value_from_secondary_if_primary_was_not_valid()
        {
            var stubPrimary = new StubIdService(null);
            var secondary = new StubIdService("valid dummy value");

            var sut = new CompositeIdService(stubPrimary, secondary);

            var actual = sut.GetIdFrom(new object());

            Assert.AreEqual("valid dummy value", actual);
        }

        [Test]
        public void returns_null_if_both_services_does_not_give_a_valid_result()
        {
            var stubPrimary = new StubIdService(null);
            var stubSecondary = new StubIdService(null);

            var sut = new CompositeIdService(stubPrimary, stubSecondary);

            var actual = sut.GetIdFrom(new object());

            Assert.IsNull(actual);
        }

        [Test]
        public void throws_exception_if_initialized_with_null_as_primary_service()
        {
            Assert.Throws<ArgumentNullException>(() => new CompositeIdService(null, new Mock<IIdService>().Object));
        }

        [Test]
        public void throws_exception_if_initialized_with_null_as_secondary_service()
        {
            Assert.Throws<ArgumentNullException>(() => new CompositeIdService(new Mock<IIdService>().Object, null));
        }
    }

    public class SpyIdService : IIdService
    {
        public bool wasCalled = false;

        public string GetIdFrom<T>(T o) where T : class
        {
            wasCalled = true;
            return "";
        }
    }

    public class StubIdService : IIdService
    {
        private readonly string _result;

        public StubIdService(string result)
        {
            _result = result;
        }

        public string GetIdFrom<T>(T o) where T : class
        {
            return _result;
        }
    }
}