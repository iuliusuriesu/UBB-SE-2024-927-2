using Client.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Client.Model.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUser(int userId);
        Task AddUser(User user);
    }
}
