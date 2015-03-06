using StructureMapExample.EmployeeTypes;

namespace StructureMapExample.EmployeeFactory
{
    public interface IEmployeeTypeStrategy
    {
        bool IsMatch(string[] values);
        Employee CreateEmployee(string[] values);
    }
}