using System.Collections.Generic;
using System.Linq;
using StructureMapExample.EmployeeStrategy;
using StructureMapExample.EmployeeTypes;
using StructureMapExample.FileManagement;

namespace StructureMapExample.Reporting
{
    public class ReportModule : IReportModule
    {
        private readonly IFileReader _reader;
        private readonly IFileWriter _writer;
        private readonly IEmployeeFactory _employeeFactory;
        private readonly IReportOrganizer _reportOrganizer;

        public ReportModule(IEmployeeFactory employeeFactory, IReportOrganizer reportOrganizer, IFileReader reader, IFileWriter writer)
        {
            _reader = reader;
            _writer = writer;
            _employeeFactory = employeeFactory;
            _reportOrganizer = reportOrganizer;
        }

        public void BuildReports(string inputPath, string outputPath)
        {
            IEnumerable<string[]> parsedValues = _reader.ParseFile(inputPath).ToList();

            IEnumerable<Employee> employees = parsedValues
                .Select(x => _employeeFactory.GetEmployee(x)).ToList();

            IEnumerable<string> reports = _reportOrganizer.OrganizeReports(employees).ToList();

            _writer.WriteFile(outputPath, reports);
        }
    }
}