using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using StructureMap;
using StructureMapExample.Tests.Data;

namespace StructureMapExample.Tests
{
    [TestClass]
    public class NestedAndChildContainers
    {
        private IContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = new Container();
            _container.Configure(x => x.For<IService>().Use<DisposableService>());
        }

        [TestMethod]
        public void TransientAreTransientFromMainContainer()
        {
            var instance1 = _container.GetInstance<IService>();
            var instance2 = _container.GetInstance<IService>();

            instance1.ShouldNotBeSameAs(instance2);
        }

        [TestMethod]
        public void TransientsFromNestedContainerAreSingleton()
        {
            var nested = _container.GetNestedContainer();
            var instance1 = nested.GetInstance<IService>();
            var instance2 = nested.GetInstance<IService>();

            instance1.ShouldBeSameAs(instance2);
        }

        [TestMethod]
        public void TransientsFromChildContainerAreUnique()
        {
            var childContainer = _container.CreateChildContainer();
            var instance1 = childContainer.GetInstance<IService>();
            var instance2 = childContainer.GetInstance<IService>();

            instance1.ShouldNotBeSameAs(instance2);
        }

        [TestMethod]
        public void DisposeInNormal()
        {
            var instance = _container.GetInstance<IService>();
            var disposeable = instance as DisposableService;
            
            disposeable.ShouldNotBe(null);
            disposeable.IsDisposed.ShouldBe(false);

            _container.Dispose();

            disposeable.IsDisposed.ShouldBe(false);
        }

        [TestMethod]
        public void DisposeInNested()
        {
            var nestedContainer = _container.GetNestedContainer();
            nestedContainer.Configure(x => x.For<IModule>().Use<DisposableModule>());

            var serviceInstance = nestedContainer.GetInstance<IService>();
            var disposableService = serviceInstance as DisposableService;

            disposableService.ShouldNotBe(null);
            disposableService.IsDisposed.ShouldBe(false);

            var moduleInstance = nestedContainer.GetInstance<IModule>();
            var disposableModule = moduleInstance as DisposableModule;

            disposableModule.ShouldNotBe(null);
            disposableModule.IsDisposed.ShouldBe(false);

            nestedContainer.Dispose();

            disposableService.IsDisposed.ShouldBe(true);
            disposableModule.IsDisposed.ShouldBe(true);
        }

        [TestMethod]
        public void DisposeInChild()
        {
            var childContainer = _container.CreateChildContainer();
            childContainer.Configure(x => x.For<IModule>().Use<DisposableModule>());

            var serviceInstance = childContainer.GetInstance<IService>();
            var disposableService = serviceInstance as DisposableService;

            disposableService.ShouldNotBe(null);
            disposableService.IsDisposed.ShouldBe(false);

            var moduleInstance = childContainer.GetInstance<IModule>();
            var disposableModule = moduleInstance as DisposableModule;

            disposableModule.ShouldNotBe(null);
            disposableModule.IsDisposed.ShouldBe(false);

            childContainer.Dispose();

            disposableService.IsDisposed.ShouldBe(false);
            disposableModule.IsDisposed.ShouldBe(false);
        }

        [TestMethod]
        public void NestedContainerDoesNotDisposeSingletonFromParent()
        {
            var container = new Container();
            container.Configure(x => x.ForSingletonOf<IService>().Use<DisposableService>());

            var nestedContainer = container.GetNestedContainer();

            var parentService = (DisposableService)container.GetInstance<IService>();
            var nestedService = (DisposableService)nestedContainer.GetInstance<IService>();

            parentService.ShouldBeSameAs(nestedService);

            parentService.IsDisposed.ShouldBe(false);
            nestedService.IsDisposed.ShouldBe(false);

            nestedContainer.Dispose();

            parentService.IsDisposed.ShouldBe(false);
            nestedService.IsDisposed.ShouldBe(false);
        }
    }
}