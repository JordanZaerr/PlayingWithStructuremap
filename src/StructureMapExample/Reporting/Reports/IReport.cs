using System;
using System.Collections.Generic;
using StructureMapExample.EmployeeTypes;

namespace StructureMapExample.Reporting.Reports
{
    public interface IReport
    {
        string Title { get; }

        Func<IEnumerable<Employee>, IEnumerable<Employee>> AdjustEmployeesForReport { get; }

        int Order { get; }
    }
}