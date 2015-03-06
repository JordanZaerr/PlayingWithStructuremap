using System;

namespace StructureMapExample.EmployeeTypes
{
    public class Hourly : Employee
    {
        private double _hourlyRate;
        private double _hoursWorked;

        public override double Earnings
        {
            get
            {
                if (_hoursWorked < 40)
                {
                    return (((_hoursWorked - 40)*(Math.Round(_hourlyRate*1.5)) + (Math.Round(_hourlyRate*40))));
                }
                return (_hoursWorked*_hourlyRate);
            }
        }

        public double HourlyRate
        {
            get { return _hourlyRate; }
            set
            {
                AssignValueWithinRange(value, v => _hourlyRate = v, "HourlyRate");
            }
        }

        public double HoursWorked
        {
            get { return _hoursWorked; }
            set
            {
                AssignValueWithinRange(value, v => _hoursWorked = v, "HoursWorked");
            }
        }
    }
}