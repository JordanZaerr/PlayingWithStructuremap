using System;
using System.Collections.Generic;
using System.Linq;
using StructureMapExample.EmployeeTypes;

namespace StructureMapExample.Reporting.Reports
{
    public class AllEmployeesIdOrder : IReport
    {
        public string Title
        {
            get { return "All Employees -- ID Order"; }
        }

        public Func<IEnumerable<Employee>, IEnumerable<Employee>> AdjustEmployeesForReport
        {
            get { return x => x.OrderBy(e => e.Id); }
        }

        public int Order
        {
            get { return 0; }
        }
    }
}