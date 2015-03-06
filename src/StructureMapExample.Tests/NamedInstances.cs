using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using StructureMap;
using StructureMapExample.Tests.Data;

namespace StructureMapExample.Tests
{
    [TestClass]
    public class NamedInstances
    {
        private IContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = new Container();
            _container.Configure(x =>
            {
                x.For<IModule>().Add<ModuleA>().Named("A");
                x.For<IModule>().Add<ModuleB>().Named("B");
                x.For<IModule>().Use<ModuleC>().Named("C");
            });
        }

        [TestMethod]
        public void UsesLastConfiguredModule()
        {
            _container.GetInstance<IModule>().ShouldBeOfType<ModuleC>();
        }

        [TestMethod]
        public void ResolvedModuleByName()
        {
            _container.GetInstance<IModule>("B").ShouldBeOfType<ModuleB>();
        }

        [TestMethod]
        public void SetsDefaultToAnotherModule()
        {
            _container.Configure(x => x.For(typeof (IModule)).Use("A"));
            _container.GetInstance<IModule>().ShouldBeOfType<ModuleA>();
        }
    }
}