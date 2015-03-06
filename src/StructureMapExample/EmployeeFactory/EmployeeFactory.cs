using System.Collections.Generic;
using System.Linq;
using StructureMapExample.EmployeeTypes;

namespace StructureMapExample.EmployeeFactory
{
    public class EmployeeFactory : IEmployeeFactory
    {
        private readonly IEnumerable<IEmployeeTypeStrategy> _strategies;

        public EmployeeFactory(IEnumerable<IEmployeeTypeStrategy> strategies)
        {
            _strategies = strategies;
        }

        public Employee GetEmployee(string[] values)
        {
            return _strategies.First(x => x.IsMatch(values)).CreateEmployee(values);
        }
    }
}