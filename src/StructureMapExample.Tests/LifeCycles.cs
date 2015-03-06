using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using StructureMap;
using StructureMapExample.Tests.Data;

namespace StructureMapExample.Tests
{
    [TestClass]
    public class LifeCycles
    {
        private IContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = new Container();
        }

        [TestMethod]
        public void TransientIsSameWithinSingleResolveCall()
        {
            _container.Configure(x => x.For<IService>().Use<Service>());
            var parent = _container.GetInstance<Parent>();

            parent.Service.ShouldBeSameAs(parent.Child.Service);
        }

        [TestMethod]
        public void TransientIsDifferentWithinSingleResolveCall()
        {
            _container.Configure(x => x.For<IService>().Use<Service>().AlwaysUnique());
            var instance = _container.GetInstance<Parent>();

            instance.Service.ShouldNotBeSameAs(instance.Child.Service);
        }

        [TestMethod]
        public void TransientsAreSingletonsFromNestedContainer()
        {
            _container.Configure(x => x.For<IService>().Use<Service>());
            var nested = _container.GetNestedContainer();
            var instance1 = nested.GetInstance<IService>();
            var instance2 = nested.GetInstance<IService>();

            instance1.ShouldBeSameAs(instance2);
        }

        [TestMethod]
        public void TransientsAreUniqueFromNestedContainer()
        {
            _container.Configure(x => x.For<IService>().Use<Service>().AlwaysUnique());
            var nested = _container.GetNestedContainer();
            var instance1 = nested.GetInstance<IService>();
            var instance2 = nested.GetInstance<IService>();

            instance1.ShouldNotBeSameAs(instance2);
        }

        [TestMethod]
        public void TransientIsDifferentWithinSingleResolveCallFromNestedContainer()
        {
            _container.Configure(x => x.For<IService>().Use<Service>().AlwaysUnique());
            var nested = _container.GetNestedContainer();
            var instance = nested.GetInstance<Parent>();

            instance.Service.ShouldNotBeSameAs(instance.Child.Service);
        }
    }

    public class Parent
    {
        public Child Child { get; set; }
        public IService Service { get; set; }

        public Parent(Child child, IService service)
        {
            Child = child;
            Service = service;
        }
    }

    public class Child
    {
        public IService Service { get; set; }

        public Child(IService service)
        {
            Service = service;
        }
    }
}