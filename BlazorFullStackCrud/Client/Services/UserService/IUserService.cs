namespace BlazorFullStackCrud.Client.Services.UserService
{
    public interface IUserService
    {
        List<User> Users { get; set; }

        List<Role> Roles { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        Task GetUsers();
        Task LoginUser();

    }
}
