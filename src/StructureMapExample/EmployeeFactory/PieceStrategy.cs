using System;
using StructureMapExample.EmployeeTypes;
using StructureMapExample.Utils;

namespace StructureMapExample.EmployeeFactory
{
    public class PieceStrategy : IEmployeeTypeStrategy
    {
        private readonly IDateParser _dateParser;

        public PieceStrategy(IDateParser dateParser)
        {
            _dateParser = dateParser;
        }

        public bool IsMatch(string[] values)
        {
            return values[0] == EmployeeType.PieceRate;
        }

        public Employee CreateEmployee(string[] values)
        {
            return new Piece 
            {
                Id = Int32.Parse(values[1]),
                LastName = values[2],
                FirstName = values[3],
                HireDate = _dateParser.GetDateTime(values[4]),
                Rate = Double.Parse(values[5]),
                Quantity = Double.Parse(values[6])
            };
        }
    }
}