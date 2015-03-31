namespace StructureMapExample.EmployeeStrategy
{
    public interface IEmployeeTypeStrategy
    {
        bool IsMatch(string[] values);
        EmployeeTypes.Employee CreateEmployee(string[] values);
    }
}