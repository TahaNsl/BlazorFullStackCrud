using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace BlazorFullStackCrud.Client.Services.UserService
{
    public class UserService : IUserService
    {
        public List<User> Users { get; set; } = new List<User>();
        public List<Role> Roles { get; set; } = new List<Role>();
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        private HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public UserService()
        {

        }

        public UserService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }



        public async Task GetUsers()
        {
            var result = await _http.GetFromJsonAsync<List<User>>("user");
            if (result != null)
            {
                Users = result;
            }
        }

        public async Task LoginUser()
        {
            await _http.PostAsJsonAsync<User>("user/loginuser", this);
        }

        public static implicit operator UserService(User user)
        {
            return new UserService
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password
            };
        }

        public static implicit operator User(UserService userService)
        {
            return new User
            {
                UserName = userService.UserName,
                Email = userService.Email,
                Password = userService.Password
            };
        }

    }
}
