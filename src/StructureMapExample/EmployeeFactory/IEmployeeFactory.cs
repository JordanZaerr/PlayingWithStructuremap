using StructureMapExample.EmployeeTypes;

namespace StructureMapExample.EmployeeFactory
{
    public interface IEmployeeFactory
    {
        Employee GetEmployee(string[] values);
    }
}