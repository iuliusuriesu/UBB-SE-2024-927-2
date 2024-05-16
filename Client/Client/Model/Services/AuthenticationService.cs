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

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            User user = await this.GetUserByUsername(username);
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
            User user = new User(0, username, nickname, password, userType);
            _userRepository.AddUser(user);
        }

        public async Task<string> GetUserType(string username)
        {
            User user = await this.GetUserByUsername(username);
            return user.UserType;
        }

        private async Task<User> GetUserByUsername(string username)
        {
            List<User> allUsers = await _userRepository.GetAllUsers();
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
