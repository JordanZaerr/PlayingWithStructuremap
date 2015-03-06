using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMapExample.Tests.Data;

namespace StructureMapExample.Tests
{
    [TestClass]
    public class Profiles
    {
        private IContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = new Container(new TestRegistry());
        }

        [TestMethod]
        public void UsesActualProfileImplementations()
        {
            var childContainer = _container.GetProfile(ProfileType.Actual);
            childContainer.GetInstance<IService>().ShouldBeOfType<Service>();
            childContainer.GetInstance<IModule>().ShouldBeOfType<ModuleA>();
        }

        [TestMethod]
        public void UsesInMemoryProfileImplementations()
        {
            var childContainer = _container.GetProfile(ProfileType.InMemory);
            childContainer.GetInstance<IService>().ShouldBeOfType<InMemoryService>();
            childContainer.GetInstance<IModule>().ShouldBeOfType<ModuleB>();
        }
    }

    public class TestRegistry : Registry
    {
        public TestRegistry()
        {
            this.Profile(ProfileType.Actual,
                x =>
                {
                    x.For<IService>().Use<Service>();
                    x.For<IModule>().Use<ModuleA>();
                });
            this.Profile(ProfileType.InMemory,
                x =>
                {
                    x.For<IService>().Use<InMemoryService>();
                    x.For<IModule>().Use<ModuleB>();
                });
        }
    }

    public static class ProfileType
    {
        public const string Actual = "Actual";
        public const string InMemory = "InMemory";
    }
}