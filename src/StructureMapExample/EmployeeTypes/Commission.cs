using System;

namespace StructureMapExample.EmployeeTypes
{
    public class Commission : Employee
    {
        private double _salary, _rate, _quantity;

        public double Salary 
        {
            get { return this._salary; }
            set
            {
                AssignValueWithinRange(value, v => _salary = v, "Salary");
            }
        }

        public override double Earnings
        {
            get { return (_salary + Math.Round(_rate*_quantity)); }
        }

        public double Rate
        {
            get { return this._rate; }
            set
            {
                AssignValueWithinRange(value, v => _rate = v, "Rate");
            }
        }

        public double Quantity
        {
            get { return this._quantity; }
            set
            {
                AssignValueWithinRange(value, v => _quantity = v, "Quantity");
            }
        }
    }
}