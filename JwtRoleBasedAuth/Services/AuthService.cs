using JwtRoleBasedAuth.Context;
using JwtRoleBasedAuth.Interfaces;
using JwtRoleBasedAuth.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtRoleBasedAuth.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(JwtContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public Role AddRole(Role role)
        {
            var data = _context.Roles.Add(role);
            _context.SaveChanges();
            return data.Entity;
        }

        public User AddUser(User user)
        {
            var data = _context.Users.Add(user);
            _context.SaveChanges();
            return data.Entity;
        }

        public bool AssignRoleToUser(AssignUserRole obj)
        {
            var userRoles = new List<UserRole>();
            var user = _context.Users.SingleOrDefault(x => x.Id == obj.UserId);
            if(user != null)
            {
                foreach(int role in obj.RoleIds)
                {
                    var userRole = new UserRole();
                    userRole.UserId = user.Id;
                    userRole.RoleId = role;
                    userRoles.Add(userRole);
                }
                _context.UserRoles.AddRange(userRoles);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public string Login(LoginRequest login)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == login.UserName && x.Password == login.Password);
            if(user != null)
            {
                // Creating claims object for user claims
                var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    // We can add any more user info in claims.
                };
                var userRoles = _context.UserRoles.Where(x => x.UserId == user.Id).ToList(); // Retriving user roles Ids
                var roldIds = userRoles.Select(x => x.RoleId).ToList(); // Getting only Role Ids from UserRoles

                //now getting all role names from Roles table based on RoleIds we got
                var roles = _context.Roles.Where(x => roldIds.Contains(x.Id)).ToList();

                //Adding roles to claims object now from table
                foreach(var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])); // Get key
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Encrypting key using HmcSha256 algo
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    signingCredentials: signIn,
                    expires: DateTime.UtcNow.AddMinutes(10)
                    ); // Created token by passing all composed details in it

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token); // created string token form composed token
                return jwtToken;
            }
            throw new Exception("Invalid user credentials!");
        }
    }
}
