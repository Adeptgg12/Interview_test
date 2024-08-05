using Interview_Test.Models;

namespace Interview_Test.Repositories.Interfaces;

public interface IUserRepository
{
    dynamic GetUserById(string id);
    int CreateUser(CreateUserReq user);
    bool ExitUserById(string id);
}