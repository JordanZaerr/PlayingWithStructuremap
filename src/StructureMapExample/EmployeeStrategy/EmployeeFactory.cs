using System.Collections.Generic;
using System.Linq;

namespace StructureMapExample.EmployeeStrategy
{
    public class EmployeeFactory : IEmployeeFactory
    {
        private readonly IEnumerable<IEmployeeTypeStrategy> _strategies;

        public EmployeeFactory(IEnumerable<IEmployeeTypeStrategy> strategies)
        {
            _strategies = strategies;
        }

        public EmployeeTypes.Employee GetEmployee(string[] values)
        {
            return _strategies.First(x => x.IsMatch(values)).CreateEmployee(values);
        }
    }
}