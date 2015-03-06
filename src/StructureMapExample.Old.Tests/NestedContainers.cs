using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using StructureMap;

namespace StructureMapExample.Old.Tests
{
    [TestClass]
    public class NestedContainers
    {
        private IContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = new Container();
            _container.Configure(x => x.For<IService>().Use<Service>());
        }

        [TestMethod]
        public void TransientAreTransientFromMainContainer()
        {
            var instance1 = _container.GetInstance<IService>();
            var instance2 = _container.GetInstance<IService>();

            instance1.ShouldNotBeSameAs(instance2);
        }

        [TestMethod]
        public void TransientsAreSingletonsFromNestedContainer()
        {
            var nested = _container.GetNestedContainer();
            var instance1 = nested.GetInstance<IService>();
            var instance2 = nested.GetInstance<IService>();

            instance1.ShouldBeSameAs(instance2);
        }
    }

    public interface IService { }

    public class Service : IService { }
}