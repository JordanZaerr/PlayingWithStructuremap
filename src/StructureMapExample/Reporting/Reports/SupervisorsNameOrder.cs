using System;
using System.Collections.Generic;
using System.Linq;
using StructureMapExample.EmployeeTypes;

namespace StructureMapExample.Reporting.Reports
{
    public class SupervisorsNameOrder : IReport
    {
        public string Title
        {
            get { return "Supervisor Employees -- Name Order"; }
        }

        public Func<IEnumerable<Employee>, IEnumerable<Employee>> AdjustEmployeesForReport
        {
            get
            {
                return x => x
                    .OfType<Supervisor>()
                    .OrderBy(e => e.LastName)
                    .ThenBy(e => e.FirstName)
                    .ThenBy(e => e.Id);
            }
        }

        public int Order
        {
            get { return 25; }
        }
    }
}