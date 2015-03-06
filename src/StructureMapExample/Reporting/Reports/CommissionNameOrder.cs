using System;
using System.Collections.Generic;
using System.Linq;
using StructureMapExample.EmployeeTypes;

namespace StructureMapExample.Reporting.Reports
{
    public class CommissionNameOrder : IReport
    {
        public string Title
        {
            get { return "Commission Employees -- Name Order"; }
        }

        public Func<IEnumerable<Employee>, IEnumerable<Employee>> AdjustEmployeesForReport
        {
            get
            {
                return x => x
                    .OfType<Commission>()
                    .OrderBy(e => e.LastName)
                    .ThenBy(e => e.FirstName)
                    .ThenBy(e => e.Id);
            }
        }

        public int Order
        {
            get { return 35; }
        }
    }
}