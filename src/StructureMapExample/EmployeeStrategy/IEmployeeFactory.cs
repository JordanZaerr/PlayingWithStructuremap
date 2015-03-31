namespace StructureMapExample.EmployeeStrategy
{
    public interface IEmployeeFactory
    {
        EmployeeTypes.Employee GetEmployee(string[] values);
    }
}