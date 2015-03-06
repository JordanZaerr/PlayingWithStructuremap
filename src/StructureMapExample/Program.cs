using StructureMap;
using StructureMapExample.Reporting;

namespace StructureMapExample
{
    internal class Program
    {
        private static void Main()
        {
            var container = new Container(new StructureMapRegistry());
            var reportModule = container.GetInstance<IReportModule>();
            //The 'OutputReport.txt' will be in the 'bin' folder
            reportModule.BuildReports("EmployeeData.txt", "OutputReport.txt");
        }
    }
}
