using TimeForARound.Dto;
using TimeForARound.Entities;

namespace TimeForARound.Services;

public interface IUserRepo
{
    public IList<User> GetAllUsers();

    public User? GetUser(string? username);
    
    public User AddUser(User user);
    
    public Round AddRound(Round round);
    public Round SetPaid(Guid roundId, bool paid);
}