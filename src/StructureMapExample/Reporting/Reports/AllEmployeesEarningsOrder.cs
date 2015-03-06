using System;
using System.Collections.Generic;
using System.Linq;
using StructureMapExample.EmployeeTypes;

namespace StructureMapExample.Reporting.Reports
{
    public class AllEmployeesEarningsOrder : IReport
    {
        public string Title
        {
            get { return "All Employees -- Earnings Order"; }
        }

        public Func<IEnumerable<Employee>, IEnumerable<Employee>> AdjustEmployeesForReport
        {
            get { return x => x.OrderBy(e => e.Earnings); }
        }

        public int Order
        {
            get { return 15; }
        }
    }
}