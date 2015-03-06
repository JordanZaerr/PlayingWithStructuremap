using System;
using System.Collections.Generic;
using System.Linq;
using StructureMapExample.EmployeeTypes;

namespace StructureMapExample.Reporting.Reports
{
    public class AllEmployeesTypeOrder : IReport
    {
        public string Title
        {
            get { return "All Employees -- Type Order"; }
        }

        public Func<IEnumerable<Employee>, IEnumerable<Employee>> AdjustEmployeesForReport
        {
            get { return x => x.OrderBy(e => e.GetType().Name); }
        }

        public int Order
        {
            get { return 20; }
        }
    }
}