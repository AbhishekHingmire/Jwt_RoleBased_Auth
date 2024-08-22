using JwtRoleBasedAuth.Models;

namespace JwtRoleBasedAuth.Interfaces
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployees();
        public Employee AddEmployee(Employee emp);
    }
}
