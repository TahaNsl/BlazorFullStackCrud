namespace BlazorFullStackCrud.Client.Services.UserService
{
    public interface IUserService
    {
        List<User> Users { get; set; }

        List<Role> Roles { get; set; }

        Task<User> GetSingleUser(int id);

        Task CreateUser(User user);

        Task GetUsers();    
    }
}
