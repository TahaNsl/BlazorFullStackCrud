using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace BlazorFullStackCrud.Client.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }

        private HttpClient _httpClient;

        public ProfileService()
        {

        }

        public ProfileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task UpdateProfile()
        {
            User user = this;
            await _httpClient.PutAsJsonAsync("user/updateprofile/" + user.Id, user);
            this.Message = "Profile updated successfully";
        }

        public async Task GetProfile()
        {
            User user = await _httpClient.GetFromJsonAsync<User>("user/getprofile/" + this.Id);
            LoadCurrentObject(user);
            this.Message = "Profile loaded successfully";
        }
        private void LoadCurrentObject(ProfileService profileService)
        {
            this.UserName = profileService.UserName;
            this.Email = profileService.Email;
            this.Password = profileService.Password;
            //add more fields
        }

        public static implicit operator ProfileService(User user)
        {
            return new ProfileService
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Id = user.Id,
            };
        }

        public static implicit operator User(ProfileService profileService)
        {
            return new User
            {
                UserName = profileService.UserName,
                Email = profileService.Email,
                Password = profileService.Password,
                Id = profileService.Id,
            };
        }
    }
}