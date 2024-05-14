using Client.Model.Entities;
using System.Collections.Generic;

namespace Client.Model.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> Users;

        public UserRepository()
        {
            Users = new List<User>
            {
                new User(1, "mihnea_turcu", "mihh", "parola123", "admin"),
                new User(2, "iulius_uriesu", "iulius", "parola456", "basic"),
            };
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public List<User> GetAllUsers()
        {
            return Users;
        }

        public User GetUser(string username)
        {
            User foundUser = Users.Find(user => user.Username == username);
            return foundUser;
        }
    }
}
