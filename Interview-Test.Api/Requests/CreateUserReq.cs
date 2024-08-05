public class CreateUserReq
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? Age { get; set; }
    
    public List<Role> Roles { get; set; }
    public List<string> Permissions { get; set; }

    
}
public class Role
{
    public int RoleId { get; set; }
    public string RoleName { get; set; }
}