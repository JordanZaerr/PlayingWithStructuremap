using System.Collections.Generic;
using StructureMapExample.EmployeeTypes;

namespace StructureMapExample.Reporting
{
    public interface IReportOrganizer
    {
        IEnumerable<string> OrganizeReports(IEnumerable<Employee> allEmployees);
    }
}