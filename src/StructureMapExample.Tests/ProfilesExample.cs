using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMapExample.EmployeeStrategy;
using StructureMapExample.FileManagement;
using StructureMapExample.Reporting;
using StructureMapExample.Reporting.Reports;
using StructureMapExample.Utils;

namespace StructureMapExample.Tests
{
    [TestClass]
    public class ProfilesExample
    {
        private IContainer _container;

        [TestInitialize]
        public void Setup()
        {
            Console.Clear();
            _container = new Container(new ProfilesRegistry());
        }

        [TestMethod]
        public void RealCode()
        {
            var childContainer = _container.GetProfile(ProfileType.Actual);
            var reportModule = childContainer.GetInstance<IReportModule>();
            //The 'ActualOutputReport.txt' will be in the 'bin' folder
            reportModule.BuildReports("EmployeeData.txt", "ActualOutputReport.txt");
        }

        [TestMethod]
        public void DebugCode()
        {
            var childContainer = _container.GetProfile(ProfileType.Debug);
            var reportModule = childContainer.GetInstance<IReportModule>();
            //The 'DebugOutputReport.txt' will be in the 'bin' folder
            reportModule.BuildReports("EmployeeData.txt", "DebugOutputReport.txt");
        }
    }

    public class ProfilesRegistry : Registry
    {
        public ProfilesRegistry()
        {
            this.Profile(ProfileType.Actual,
                x =>
                {
                    //File processing
                    x.For<IFileReader>().Use<DelimitedFileReader>().Ctor<char>("delimiter").Is(' ');
                    x.For<IFileWriter>().Use<FileWriter>();
                    x.For<IDateParser>().Use<DateParser>();

                    //Reporting
                    x.For<IReport>().Use<AllEmployeesIdOrder>();
                    x.For<IReportModule>().Use<ReportModule>();
                    x.For<IReportGenerator>().Use<ReportGenerator>();
                    x.For<IReportOrganizer>().Use<ReportOrganizer>();

                    //Employee
                    x.For<IEmployeeTypeStrategy>().Add<CommissionStrategy>();
                    x.For<IEmployeeTypeStrategy>().Add<HourlyStrategy>();
                    x.For<IEmployeeTypeStrategy>().Add<PieceStrategy>();
                    x.For<IEmployeeTypeStrategy>().Add<SupervisorStrategy>();
                    x.For<IEmployeeFactory>().Use<EmployeeFactory>();
                });
            this.Profile(ProfileType.Debug,
                x =>
                {
                    //File processing
                    x.For<IFileReader>().WrapWithLogger<IFileReader, DelimitedFileReader>(new DelimitedFileReader(' '));
                    x.For<IFileWriter>().WrapWithLogger<IFileWriter, FileWriter>();
                    x.For<IDateParser>().WrapWithLogger<IDateParser, DateParser>();

                    //Reporting
                    x.For<IReport>().WrapWithLogger<IReport, AllEmployeesIdOrder>();
                    x.For<IReportModule>().WrapWithLogger<IReportModule, ReportModule>();
                    x.For<IReportGenerator>().WrapWithLogger<IReportGenerator, ReportGenerator>();
                    x.For<IReportOrganizer>().WrapWithLogger<IReportOrganizer, ReportOrganizer>();

                    //Employee
                    x.For<IEmployeeTypeStrategy>().WrapWithLogger<IEmployeeTypeStrategy, CommissionStrategy>();
                    x.For<IEmployeeTypeStrategy>().WrapWithLogger<IEmployeeTypeStrategy, HourlyStrategy>();
                    x.For<IEmployeeTypeStrategy>().WrapWithLogger<IEmployeeTypeStrategy, PieceStrategy>();
                    x.For<IEmployeeTypeStrategy>().WrapWithLogger<IEmployeeTypeStrategy, SupervisorStrategy>();
                    x.For<IEmployeeFactory>().WrapWithLogger<IEmployeeFactory, EmployeeFactory>();
                });
        }
    }
}