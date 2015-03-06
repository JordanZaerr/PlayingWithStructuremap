using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using StructureMap;
using StructureMap.Building;
using StructureMapExample.Tests.Data;

namespace StructureMapExample.Tests
{
    [TestClass]
    public class ForwardAndRedirects
    {
        private readonly IContainer _container = new Container();

        [TestMethod]
        public void ResolvingATypeThatImplementsTwoInterfacesGivesTwoSeparateInstances()
        {
            _container.Configure(x => 
            {
                x.ForSingletonOf<IModule>().Use<ModuleA>();
                x.ForSingletonOf<IModuleA>().Use<ModuleA>();
            });

            _container.GetInstance<IModule>()
                .ShouldNotBeSameAs(_container.GetInstance<IModuleA>());
        }

        [TestMethod]
        public void ResolvingTwoInterfacesResultsInTheSameInstance()
        {
            _container.Configure(x => 
            {
                x.ForSingletonOf<IModule>().Use<ModuleA>();
                x.Forward<IModule, IModuleA>();
            });

            _container.GetInstance<IModule>()
                .ShouldBeSameAs(_container.GetInstance<IModuleA>());
        }

        [TestMethod]
        public void RedirectsToAnotherType()
        {
            _container.Configure(x =>
            { 
                x.ForSingletonOf<IModule>().Use<ModuleA>();
                x.Redirect<IModuleA, IModule>();
            });
            var instance = _container.GetInstance<IModuleA>();

            instance.ShouldBeOfType<ModuleA>();
            _container.GetInstance<IModule>()
                .ShouldBeSameAs(_container.GetInstance<IModuleA>());
        }

        [TestMethod]
        public void ForwardFailure()
        {
            _container.Configure(x =>
            {
                x.For<IService>().Use<Service>();
                x.Forward<IModule, IService>();
            });

            Should.Throw<StructureMapConfigurationException>(() =>
                _container.GetInstance<IModule>());
        }

        [TestMethod]
        public void RedirectFailure()
        {
            _container.Configure(x =>
            {
                x.For<IService>().Use<Service>();
                x.Redirect<IModule, IService>();
            });

            Should.Throw<StructureMapBuildException>(() =>
                _container.GetInstance<IModule>());
        }
    }
}