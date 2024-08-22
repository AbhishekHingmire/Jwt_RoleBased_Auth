using JwtRoleBasedAuth.Models;

namespace JwtRoleBasedAuth.Interfaces
{
    public interface IAuthService
    {
        User AddUser(User user);
        string Login(LoginRequest login);
        Role AddRole(Role role);
        bool AssignRoleToUser(AssignUserRole userRole);
    }
}
