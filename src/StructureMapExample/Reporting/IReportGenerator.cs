using System.Collections.Generic;
using StructureMapExample.EmployeeTypes;

namespace StructureMapExample.Reporting
{
    public interface IReportGenerator
    {
        IEnumerable<string> GenerateReport(string reportTitle, IEnumerable<Employee> employeesOnReport);
    }
}