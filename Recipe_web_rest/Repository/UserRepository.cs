using Microsoft.EntityFrameworkCore;
using Recipe_web_rest.Data;
using Recipe_web_rest.Interfaces;
using Recipe_web_rest.Models;
using Recipe_web_rest.Request;

namespace Recipe_web_rest.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int? CheckID(LoginRequest loginRequest)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.UserId == loginRequest.UserId);
            if (user != null && BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                return user.Id;
            }
            return null;
        }

        public bool CreateUser(User user)
        {
            _dataContext.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _dataContext.Remove(user);
            return Save();
        }

        public User GetUser(int id)
        {
            return _dataContext.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _dataContext.Users.ToList();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _dataContext.Update(user);
            return Save();
        }

        public bool UserExists(int id)
        {
            return _dataContext.Users.Any(u => u.Id == id);
        }
    }
}
