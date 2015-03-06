using System;

namespace StructureMapExample.EmployeeTypes
{
    public class Piece : Employee
    {
        private double _rate, _quantity;

        public override double Earnings
        {
            get { return Math.Round(_rate*_quantity); }
        }

        public double Rate
        {
            get { return _rate; }
            set
            {
                AssignValueWithinRange(value, x => _rate = x, "Rate");
            }
        }

        public double Quantity
        {
            get { return _quantity; }
            set
            {
                AssignValueWithinRange(value, x => _quantity = x, "Quantity");
            }
        }
    }
}