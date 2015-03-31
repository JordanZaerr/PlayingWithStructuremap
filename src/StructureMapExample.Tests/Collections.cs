using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using StructureMap;
using StructureMapExample.Tests.Data;

namespace StructureMapExample.Tests
{
    [TestClass]
    public class Collections
    {
        private readonly IContainer _container = new Container();
        private readonly Type[] _expectedTypes = {typeof(ModuleA), typeof(ModuleB), typeof(ModuleC)};

        [TestInitialize]
        public void Setup()
        {
            _container.Configure(x =>
            {
                x.For<IModule>().Add<ModuleA>();
                x.For<IModule>().Add<ModuleB>();
                x.For<IModule>().Add<ModuleC>();
            });
        }

        [TestMethod]
        public void CanResolveArray()
        {
            var instance = _container.GetInstance<ArrayClass>();
            instance.Collection.Select(x => x.GetType())
                .ShouldBeSameCollectionAs(_expectedTypes);
        }

        [TestMethod]
        public void CanResolveList()
        {
            var instance = _container.GetInstance<ListClass>();
            instance.Collection.Select(x => x.GetType())
                .ShouldBeSameCollectionAs(_expectedTypes);
        }

        [TestMethod]
        public void CanResolveEnumerable()
        {
            var instance = _container.GetInstance<EnumerableClass>();
            instance.Collection.Select(x => x.GetType())
                .ShouldBeSameCollectionAs(_expectedTypes);
        }

        [TestMethod]
        public void CanResolveLazyArray()
        {
            var instance = _container.GetInstance<LazyArrayClass>();
            instance.Collection.Value.Select(x => x.GetType())
                .ShouldBeSameCollectionAs(_expectedTypes);
        }

        [TestMethod]
        public void CanResolveLazyList()
        {
            var instance = _container.GetInstance<LazyListClass>();
            instance.Collection.Value.Select(x => x.GetType())
                .ShouldBeSameCollectionAs(_expectedTypes);
        }

        [TestMethod]
        public void CanResolveLazyEnumerable()
        {
            var instance = _container.GetInstance<LazyEnumerableClass>();
            instance.Collection.Value.Select(x => x.GetType())
                .ShouldBeSameCollectionAs(_expectedTypes);
        }

        [TestMethod]
        public void SuperLazy()
        {
            var instance = _container.GetInstance<SuperLazy>();
            instance.Func.ShouldNotBe(null);
            instance.Func().Value.Value().Value.Length.ShouldBe(3);
        }
    }

    public class ArrayClass
    {
        public IModule[] Collection;
        public ArrayClass(IModule[] array)
        {
            Collection = array;
        }
    }

    public class ListClass
    {
        public IList<IModule> Collection;
        public ListClass(IList<IModule> list)
        {
            Collection = list;
        }
    }

    public class EnumerableClass
    {
        public IEnumerable<IModule> Collection;
        public EnumerableClass(IEnumerable<IModule> enumerable)
        {
            Collection = enumerable;
        }
    }

    public class LazyArrayClass
    {
        public Lazy<IModule[]> Collection;
        public LazyArrayClass(Lazy<IModule[]> lazyArray)
        {
            Collection = lazyArray;
        }
    }

    public class LazyListClass
    {
        public Lazy<IList<IModule>> Collection;
        public LazyListClass(Lazy<IList<IModule>> lazyList)
        {
            Collection = lazyList;
        }
    }

    public class LazyEnumerableClass
    {
        public Lazy<IEnumerable<IModule>> Collection;
        public LazyEnumerableClass(Lazy<IEnumerable<IModule>> lazyEnumerable)
        {
            Collection = lazyEnumerable;
        }
    }

    public class SuperLazy
    {
        public Func<Lazy<Lazy<Func<Lazy<IModule[]>>>>> Func { get; set; }

        public SuperLazy(Func<Lazy<Lazy<Func<Lazy<IModule[]>>>>> func)
        {
            Func = func;
        }
    }
}