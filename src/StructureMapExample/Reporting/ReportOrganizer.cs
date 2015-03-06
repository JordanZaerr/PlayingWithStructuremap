using System.Collections.Generic;
using System.Linq;
using StructureMapExample.EmployeeTypes;
using StructureMapExample.Reporting.Reports;

namespace StructureMapExample.Reporting
{
    public class ReportOrganizer : IReportOrganizer
    {
        private readonly IReportGenerator _reportGenerator;
        private readonly IEnumerable<IReport> _reports;

        public ReportOrganizer(IReportGenerator reportGenerator, IEnumerable<IReport> reports)
        {
            _reportGenerator = reportGenerator;
            _reports = reports;
        }

        public IEnumerable<string> OrganizeReports(IEnumerable<Employee> allEmployees)
        {
            return _reports.OrderBy(x => x.Order)
                .SelectMany(x => 
                    _reportGenerator.GenerateReport(x.Title, x.AdjustEmployeesForReport(allEmployees)));
        }
    }
}