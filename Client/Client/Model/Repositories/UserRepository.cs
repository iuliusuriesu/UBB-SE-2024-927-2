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
            // POST https://localhost:7100/api/Users
            Users.Add(user);
        }

        public List<User> GetAllUsers()
        {
            // GET https://localhost:7100/api/Users
            return Users;
        }

        public User GetUser(int userId)
        {
            // GET https://localhost:7100/api/Users/:userId
            User foundUser = Users.Find(user => user.UserID == userId);
            return foundUser;
        }
    }
}
