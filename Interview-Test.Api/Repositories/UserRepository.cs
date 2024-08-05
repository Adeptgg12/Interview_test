
using Interview_Test.Infrastructure;
using Interview_Test.Models;
using Interview_Test.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Interview_Test.Repositories;


public class UserRepository : IUserRepository
{
    private readonly InterviewTestDbContext _context;

    public UserRepository(InterviewTestDbContext context)
    {
        _context = context;
    }

    public dynamic GetUserById(string id)
    {
            var includingObject = _context.UserTb
                .Include(include => include.UserProfile)
                .Include(include => include.UserRoleMappings)
                    .ThenInclude(thenInclude => thenInclude.Role)
                    .ThenInclude(thenInclude => thenInclude.Permissions)
                .Where(includingObject => includingObject.Id.ToString() == id)
                .Select(selector => new GetUserByIdResponse()
                {
                    Id = selector.Id,
                    UserId = selector.UserId,
                    Username = selector.Username,
                    FirstName = selector.UserProfile.FirstName,
                    LastName = selector.UserProfile.LastName,
                    Age = selector.UserProfile.Age,
                    Roles = selector.UserRoleMappings.Select(urm => new Role()
                    {
                        RoleId = urm.Role.RoleId,
                        RoleName = urm.Role.RoleName,
                    }).ToList(),
                    Permissions = selector.UserRoleMappings.SelectMany(prm => prm.Role.Permissions)
                        .Select(p => p.Permission).ToList()
                }).FirstOrDefault();
            return includingObject;
    }

    public bool ExitUserById(string id)
    {
        var exitUserByIdObject = _context.UserTb
            .Any(includingObject => includingObject.Id.ToString() == id);
        return exitUserByIdObject;
    }


    public int CreateUser(CreateUserReq user)
    {
        var uPermissions = user.Permissions
            .Select(permission => new PermissionModel
            {
                Permission = permission
            }).Distinct().ToList();
        var userCreate = new UserModel // สร้าง obj ของ UserModel
        {
            Id = user.Id,
            UserId = user.UserId,
            Username = user.Username,
            UserProfile = new UserProfileModel // สร้าง obj ของ profile
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
            },
            UserRoleMappings = user.Roles.Select(role => new UserRoleMappingModel
            {
                Role = new RoleModel
                {
                    RoleName = role.RoleName,
                    Permissions = uPermissions
                },
            }).ToList(),
        };
        _context.UserTb.Add(userCreate);
        return _context.SaveChanges();
    }
}



