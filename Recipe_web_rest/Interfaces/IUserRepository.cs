using Recipe_web_rest.Models;
using Recipe_web_rest.Request;

namespace Recipe_web_rest.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        int? CheckID(LoginRequest loginRequest);
        bool UserExists (int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
