using JwtRoleBasedAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtRoleBasedAuth.Context
{
    public class JwtContext : DbContext
    {
        public JwtContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users {get; set;}
        public DbSet<Role> Roles {get; set;}
        public DbSet<UserRole> UserRoles {get; set;}
        public DbSet<Employee> Employees {get; set;}
    }
}
