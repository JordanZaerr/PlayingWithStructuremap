using StructureMap.Configuration.DSL;
using StructureMapExample.EmployeeFactory;
using StructureMapExample.FileManagement;
using StructureMapExample.Reporting;
using StructureMapExample.Reporting.Reports;
using StructureMapExample.Utils;

namespace StructureMapExample
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            this.IncludeRegistry<ReportRegistry>();
            this.IncludeRegistry<EmployeeRegistry>();

            ForSingletonOf<IFileReader>().Use<DelimitedFileReader>().Ctor<char>("delimiter").Is(' ');
            ForSingletonOf<IFileWriter>().Use<FileWriter>();
            ForSingletonOf<IDateParser>().Use<DateParser>();
        }
    }

    public class ReportRegistry : Registry
    {
        public ReportRegistry()
        {
            this.Scan(x => 
            {
                x.AssemblyContainingType<IReport>();
                x.AddAllTypesOf<IReport>();
            });

            ForSingletonOf<IReportModule>().Use<ReportModule>();
            ForSingletonOf<IReportGenerator>().Use<ReportGenerator>();
            ForSingletonOf<IReportOrganizer>().Use<ReportOrganizer>();
        }
    }

    public class EmployeeRegistry : Registry
    {
        public EmployeeRegistry()
        {
            For<IEmployeeTypeStrategy>().Use<CommissionStrategy>();
            For<IEmployeeTypeStrategy>().Use<HourlyStrategy>();
            For<IEmployeeTypeStrategy>().Use<PieceStrategy>();
            For<IEmployeeTypeStrategy>().Use<SupervisorStrategy>();

            For<IEmployeeFactory>().Use<EmployeeFactory.EmployeeFactory>().Singleton();
        }
    }
}