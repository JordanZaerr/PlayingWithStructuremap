namespace StructureMapExample.EmployeeTypes
{
    public class Supervisor : Employee
    {
        private double _salary;

        public double Salary
        {
            get 
            {  
                return _salary;
            }
            set
            {
                AssignValueWithinRange(value, x => _salary = x, "Salary");
            }
        }

        public override double Earnings
        {
            get
            {
                return _salary;
            }
        }
    }
}