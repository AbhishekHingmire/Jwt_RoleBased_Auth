namespace JwtRoleBasedAuth.Models
{
    public class AssignUserRole
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
