using Client.Model.Entities;
using Client.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool AuthenticateUser(string username, string password)
        {
            User user = this.GetUserByUsername(username);
            if (user == null)
            {
                return false;
            }

            if (user.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateUser(string username, string password, string nickname, string userType)
        {
            User user = new User(0, username, password, nickname, userType);
            _userRepository.AddUser(user);
        }

        public string GetUserType(string username)
        {
            User user = this.GetUserByUsername(username);
            return user.UserType;
        }

        private User GetUserByUsername(string username)
        {
            List<User> allUsers = _userRepository.GetAllUsers();
            foreach (User user in allUsers)
            {
                if (user.Username == username)
                {
                    return user;
                }
            }

            return null;
        }
    }
}
