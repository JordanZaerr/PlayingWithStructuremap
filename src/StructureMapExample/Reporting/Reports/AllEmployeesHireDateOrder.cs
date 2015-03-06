using System;
using System.Collections.Generic;
using System.Linq;
using StructureMapExample.EmployeeTypes;

namespace StructureMapExample.Reporting.Reports
{
    public class AllEmployeesHireDateOrder : IReport
    {
        public string Title
        {
            get { return "All Employees -- Hire Date Order"; }
        }

        public Func<IEnumerable<Employee>, IEnumerable<Employee>> AdjustEmployeesForReport 
        {
            get { return x => x.OrderBy(e => e.HireDate); }
        }

        public int Order
        {
            get { return 10; }
        }
    }
}