using Client.Model.Entities;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System;
using System.Threading.Tasks;


namespace Client.Model.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> Users;

        public UserRepository()
        {
            Users = new List<User>();
            /* don't need this anymore i think
             * Users = new List<User>
            {
                new User(1, "mihnea_turcu", "mihh", "parola123", "admin"),
                new User(2, "iulius_uriesu", "iulius", "parola456", "basic"),
            };*/
        }

        public async Task AddUser(User newUser)
        {
            // POST https://localhost:7100/api/Users

            using (var httpClient = new HttpClient())
            {
                string endpoint = "https://localhost:7100/api/Users";
                var jsonSerialized = JsonConvert.SerializeObject(newUser);
                var content = new StringContent(jsonSerialized, Encoding.UTF8, "application/json");
                var res = await httpClient.PostAsync(endpoint, content);

                if (res.IsSuccessStatusCode)
                {
                    Console.WriteLine("User added successfully.");
                }
                else
                {
                    throw new Exception($"Error when trying to add a new user. Status code: {res.StatusCode}, Reason: {res.ReasonPhrase}");
                }
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            // GET https://localhost:7100/api/Users

            try
            {
                using (var httpClient = new HttpClient())
                {
                    string endpoint = "https://localhost:7100/api/Users";
                    var res = await httpClient.GetAsync(endpoint);

                    if (res.IsSuccessStatusCode)
                    {
                        var json = await res.Content.ReadAsStringAsync();
                        var resUsers = JsonConvert.DeserializeObject<List<User>>(json);
                        return resUsers;
                    }
                    else
                    {
                        throw new Exception($"Error when retrieving all users. Status code: {res.StatusCode}, Reason: {res.ReasonPhrase}");
                    }
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                throw new Exception($"Request error: {httpRequestException.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        public async Task<User> GetUser(int userId)
        {
            // GET https://localhost:7100/api/Users/:userId

            try
            {
                using (var httpClient = new HttpClient())
                {
                    string endpoint = $"https://localhost:7100/api/Users/{userId}";
                    var res = await httpClient.GetAsync(endpoint);

                    if (res.IsSuccessStatusCode)
                    {
                        var json = await res.Content.ReadAsStringAsync();
                        var foundUser = JsonConvert.DeserializeObject<User>(json);
                        return foundUser;
                    }
                    else
                    {
                        throw new Exception($"Error when retrieving a user by userId. Status code: {res.StatusCode}, Reason: {res.ReasonPhrase}");
                    }
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                throw new Exception($"Request error: {httpRequestException.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }
    }
}
