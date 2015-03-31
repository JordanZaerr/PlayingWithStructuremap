using System;
using StructureMapExample.EmployeeTypes;
using StructureMapExample.Utils;

namespace StructureMapExample.EmployeeStrategy
{
    public class SupervisorStrategy : IEmployeeTypeStrategy
    {
        private readonly IDateParser _dateParser;

        public SupervisorStrategy(IDateParser dateParser)
        {
            _dateParser = dateParser;
        }

        public bool IsMatch(string[] values)
        {
            return values[0] == EmployeeType.Supervisor;
        }

        public EmployeeTypes.Employee CreateEmployee(string[] values)
        {
            return new Supervisor 
            {
                Id = Int32.Parse(values[1]),
                LastName = values[2],
                FirstName = values[3],
                HireDate = _dateParser.GetDateTime(values[4]),
                Salary = Double.Parse(values[5])
            };
        }
    }
}