using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StructureMap.AutoMocking.Moq;
using StructureMapExample.EmployeeStrategy;
using StructureMapExample.EmployeeTypes;
using StructureMapExample.FileManagement;
using StructureMapExample.Reporting;

namespace StructureMapExample.Tests
{
    [TestClass]
    public class ReportModuleTests
    {
        private ReportModule _target;
        private MoqAutoMocker<ReportModule> _autoMocker;

        [TestInitialize]
        public void SetUp()
        {
            _autoMocker = new MoqAutoMocker<ReportModule>();
            _target = _autoMocker.ClassUnderTest;
        }

        [TestMethod]
        public void ReadDataFromFile()
        {
            _target.BuildReports("In","Out");
            
            GetMock<IFileReader>()
                .Verify(x => x.ParseFile(It.Is<string>(f => f == "In")), Times.Once());
        }

        [TestMethod]
        public void CallsGetEmployee()
        {
            SetupFileReaderReturn();

             GetMock<IReportOrganizer>().Setup(x =>
                x.OrganizeReports(It.IsAny<IEnumerable<Employee>>()))
                .Returns(new []{""});

            _target.BuildReports("In","Out");
            GetMock<IEmployeeFactory>()
                .Verify(x => x.GetEmployee(It.IsAny<string[]>()), Times.Once());
        }

        [TestMethod]
        public void CallsOrganizeReports()
        {
            SetupFileReaderReturn();
            SetupGetEmployeeReturn();
            _target.BuildReports("In", "Out");
            GetMock<IReportOrganizer>().Verify(x => 
                x.OrganizeReports(It.Is<IEnumerable<Employee>>(e => e.Count() == 1)), Times.Once());
        }

        [TestMethod]
        public void WritesOutReportToFile()
        {
            SetupFileReaderReturn();
            SetupGetEmployeeReturn();
            SetupOrganizeReportsReturn();

            _target.BuildReports("In", "Out");

            GetMock<IFileWriter>().Verify(x => 
                x.WriteFile(
                    It.Is<string>(p => p == "Out"), 
                    It.Is<IEnumerable<string>>(s => s.Count() == 1)), Times.Once());
        }

        [TestMethod]
        public void FactoryTest_ThisIsUseless()
        {
            var factory = new MoqFactory();

            var mock = Mock.Get((IReportModule)factory.CreateMock(typeof (IReportModule)));
            mock.Object.BuildReports("In", "Out");

            mock.Verify(x => x.BuildReports(It.IsAny<string>(), It.IsAny<string>()));
        }

        private void SetupFileReaderReturn()
        {
            GetMock<IFileReader>().Setup(x => x.ParseFile(It.IsAny<string>()))
                .Returns(new List<string[]> { new []{""} });
        }

        private void SetupGetEmployeeReturn()
        {
            GetMock<IEmployeeFactory>().Setup(x => x.GetEmployee(It.IsAny<string[]>()))
                .Returns(new Hourly());
        }

        private void SetupOrganizeReportsReturn()
        {
            GetMock<IReportOrganizer>().Setup(x => x.OrganizeReports(It.IsAny<IEnumerable<Employee>>()))
                .Returns(new List<string> { "" });
        }

        private Mock<T> GetMock<T>() where T: class
        {
            return Mock.Get(_autoMocker.Get<T>());
        }
    }
}
