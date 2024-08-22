using JwtRoleBasedAuth.Interfaces;
using JwtRoleBasedAuth.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtRoleBasedAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }
        [HttpPost("Login")]
        public string Login([FromBody] LoginRequest obj)
        {
            var token = _auth.Login(obj);
            return token;
        }

        [HttpPost("AddUser")]
        public User AddUser([FromBody] User obj)
        {
            var user = _auth.AddUser(obj);
            return user;
        }

        [HttpPost("AddRole")]
        public Role AddRole([FromBody] Role obj)
        {
            var role = _auth.AddRole(obj);
            return role;
        }

        [HttpPost("AssignRoleToUser")]
        public bool AssignUserRole([FromBody] AssignUserRole obj)
        {
            var role = _auth.AssignRoleToUser(obj);
            return role;
        }
    }
}
