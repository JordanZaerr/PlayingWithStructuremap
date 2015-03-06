using System;
using StructureMapExample.EmployeeTypes;
using StructureMapExample.Utils;

namespace StructureMapExample.EmployeeFactory
{
    public class CommissionStrategy : IEmployeeTypeStrategy
    {
        private readonly IDateParser _dateParser;

        public CommissionStrategy(IDateParser dateParser)
        {
            _dateParser = dateParser;
        }

        public bool IsMatch(string[] values)
        {
            return values[0] == EmployeeType.Commission;
        }

        public Employee CreateEmployee(string[] values)
        {
            return new Commission 
            {
                Id = Int32.Parse(values[1]),
                LastName = values[2],
                FirstName = values[3],
                HireDate = _dateParser.GetDateTime(values[4]),
                Salary = Double.Parse(values[5]),
                Rate = Double.Parse(values[6]),
                Quantity = Double.Parse(values[7])
            };
        }
    }
}