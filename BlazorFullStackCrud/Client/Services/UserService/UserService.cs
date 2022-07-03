using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorFullStackCrud.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public UserService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<User> Users { get; set; } = new List<User>();
        public List<Role> Roles { get; set; } = new List<Role>();

        public async Task CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetSingleUser(string UserName)
        {
            throw new NotImplementedException();
        }

        public async Task GetUsers()
        {
            var result = await _http.GetFromJsonAsync<List<User>>("api/user");
            if (result != null)
            {
                Users = result;
            }
        }
    }
}
