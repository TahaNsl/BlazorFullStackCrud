namespace BlazorFullStackCrud.Client.Services.UserService
{
    public interface IUserService
    {
        List<User> Users { get; set; }

        List<Role> Roles { get; set; }

        Task GetUsers();
        Task LoginUser();

    }
}
