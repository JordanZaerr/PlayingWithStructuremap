using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace StructureMapExample.Tests
{
    [TestClass]
    public class ProfilesPassThrough
    {
        private IContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = new Container(new ProfileYearRegistry());
        }

        [TestMethod]
        public void WorksFor2015()
        {
            var handler = _container.GetInstance<IRequestHandler>();
            var result = handler.Handle(new Request {Year = 2015});
            result.ShouldBe("Data for 2015");
        }

        [TestMethod]
        public void WorksFor2016()
        {
            var handler = _container.GetInstance<IRequestHandler>();
            var result = handler.Handle(new Request {Year = 2016});
            result.ShouldBe("Data for 2016");
        }

        [TestMethod]
        public void WorksFor2017NotSpecificProvider()
        {
            var handler = _container.GetInstance<IRequestHandler>();
            var result = handler.Handle(new Request { Year = 2017 });
            result.ShouldBe("Data for default");
        }
    }

    public class ProfileYearRegistry : Registry
    {
        public ProfileYearRegistry()
        {
            For<IDataProviderFactory>().Use<DataProviderFactory>();
            For<IRequestHandler>().Use<RequestHandler>();
            For<IDataProvider>().Use<DefaultDataProvider>();

            Profile("2015", x => x.For<IDataProvider>().Use<DataProvider2015>());
            Profile("2016", x => x.For<IDataProvider>().Use<DataProvider2016>());
        }
    }

    //=========
    //Providers
    //=========
    public interface IDataProvider
    {
        string GetData();
    }

    public class DefaultDataProvider : IDataProvider
    {
        public string GetData()
        {
            return "Data for default";
        }
    }

    public class DataProvider2015 : IDataProvider
    {
        public string GetData()
        {
            return "Data for 2015";
        }
    }

    public class DataProvider2016 : IDataProvider
    {
        public string GetData()
        {
            return "Data for 2016";
        }
    }

    //=======
    //Factory
    //=======
    public interface IDataProviderFactory
    {
        IDataProvider GetProvider(int year);
    }

    public class DataProviderFactory : IDataProviderFactory
    {
        private readonly IContainer _container;

        public DataProviderFactory(IContainer container)
        {
            _container = container;
        }

        public IDataProvider GetProvider(int year)
        {
            var childContainer = _container.GetProfile(year.ToString());
            var role = childContainer.Role;
            return childContainer.GetInstance<IDataProvider>();
        }
    }

    //=======
    //Handler
    //=======
    public class Request
    {
        public int Year { get; set; }
    }

    public interface IRequestHandler
    {
        string Handle(Request request);
    }

    public class RequestHandler : IRequestHandler
    {
        private readonly IDataProviderFactory _factory;

        public RequestHandler(IDataProviderFactory factory)
        {
            _factory = factory;
        }

        public string Handle(Request request)
        {
            return _factory.GetProvider(request.Year).GetData();
        }
    }
}