using JwtRoleBasedAuth.Interfaces;
using JwtRoleBasedAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtRoleBasedAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _empl;

        public EmployeeController(IEmployeeService empl)
        {
            _empl = empl;
        }

        [HttpPost("AddEmployee")]
        public Employee AddEmployee([FromBody] Employee obj)
        {
            var emp = _empl.AddEmployee(obj);
            return emp;
        }

        [HttpGet("GetEmployees")]
        public List<Employee> GetEmployees()
        {
            var empDetails = _empl.GetEmployees();
            return empDetails;
        }
    }
}
