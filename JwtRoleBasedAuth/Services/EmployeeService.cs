using JwtRoleBasedAuth.Context;
using JwtRoleBasedAuth.Interfaces;
using JwtRoleBasedAuth.Models;

namespace JwtRoleBasedAuth.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly JwtContext _context;
        public EmployeeService(JwtContext context)
        {
            _context = context;
        }
        public Employee AddEmployee(Employee emp)
        {
           var data = _context.Employees.Add(emp);
           _context.SaveChanges();
            return data.Entity;
        }

        public List<Employee> GetEmployees()
        {
            var data = _context.Employees.ToList();
            return data;
        }
    }
}
