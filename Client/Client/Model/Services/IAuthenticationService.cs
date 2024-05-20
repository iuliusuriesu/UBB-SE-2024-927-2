using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.Services
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateUser(string username, string password);
        void CreateUser(string username, string password, string nickname, string userType);
        Task<string> GetUserType(string username);
    }
}
