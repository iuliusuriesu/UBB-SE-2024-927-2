using Client.Model.Entities;
using System.Collections.Generic;

namespace Client.Model.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUser(int userId);
        void AddUser(User user);
    }
}
