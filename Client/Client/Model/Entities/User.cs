using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }

        public User(int userID, string username, string nickname, string password, string userType)
        {
            UserID = userID;
            Username = username;
            Nickname = nickname;
            Password = password;
            UserType = userType;
        }
    }
}
