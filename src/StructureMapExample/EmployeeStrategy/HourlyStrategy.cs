using System;
using StructureMapExample.EmployeeTypes;
using StructureMapExample.Utils;

namespace StructureMapExample.EmployeeStrategy
{
    public class HourlyStrategy : IEmployeeTypeStrategy
    {
        private readonly IDateParser _dateParser;

        public HourlyStrategy(IDateParser dateParser)
        {
            _dateParser = dateParser;
        }

        public bool IsMatch(string[] values)
        {
            return values[0] == EmployeeType.Hourly;
        }

        public EmployeeTypes.Employee CreateEmployee(string[] values)
        {
            return new Hourly 
            {
                Id = Int32.Parse(values[1]),
                LastName = values[2],
                FirstName = values[3],
                HireDate = _dateParser.GetDateTime(values[4]),
                HourlyRate = Double.Parse(values[5]),
                HoursWorked = Double.Parse(values[6])
            };
        }
    }
}