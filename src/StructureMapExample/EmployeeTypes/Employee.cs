using System;

namespace StructureMapExample.EmployeeTypes
{
    public abstract class Employee
    {
        private const double FedTaxRate = 0.22;
        private const double FicaRate = 0.07;
        private const double StateTaxRate = 0.05;
        private string _firstName;
        private int _id;
        private string _lastName;

        public abstract double Earnings { get; }

        public double FICA
        {
            get { return Math.Round(FicaRate*this.Earnings); }
        }

        public double FedTax
        {
            get { return Math.Round(FedTaxRate*this.Earnings); }
        }

        public string FirstName
        {
            get { return this._firstName; }
            set { this._firstName = value.Trim(); }
        }

        public DateTime HireDate { get; set; }

        public int Id
        {
            get { return this._id; }
            set
            {
                AssignValueWithinRange(value, x => _id = (int)x, "Id", 1, 9999);
            }
        }

        public string LastName
        {
            get { return this._lastName; }
            set { this._lastName = value.Trim(); }
        }

        public double NetPay 
        {
            get { return this.Earnings - this.StateTax - this.FICA - this.FedTax; }
        }

        public double StateTax
        {
            get { return Math.Round(StateTaxRate*this.Earnings); }
        }

        protected void AssignValueWithinRange(
            double value, Action<double> setter, string propertyName, 
            double min = 0.0, double max = double.MaxValue)
        {
            if (value >= min && value <= max)
                setter(value);
            else
                Console.WriteLine("{0} can not be assigned to the {1} property", value, propertyName);
        }

        public override string ToString()
        {
            return String.Format("Name: {0} {1}, EmployeeType: {2}, Pay: ${3}", FirstName, LastName, this.GetType().Name, NetPay);
        }
    }
}