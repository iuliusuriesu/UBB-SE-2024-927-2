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
            User user = _userRepository.GetUser(username);
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
    }
}
