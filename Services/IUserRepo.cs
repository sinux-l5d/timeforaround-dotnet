using TimeForARound.Entities;

namespace TimeForARound.Services;

public interface IUserRepo
{
    public IList<User> GetAllUsers();

    public User? GetUser(string? username);
    
    public User AddUser(User user);
}