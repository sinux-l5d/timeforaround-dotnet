using Microsoft.EntityFrameworkCore;
using TimeForARound.Data;
using TimeForARound.Entities;

namespace TimeForARound.Services;

public class SQLiteUserRepo : IUserRepo
{
    private readonly DataContext _context;
    
    public SQLiteUserRepo(DataContext context)
    {
        _context = context;
    }
    public IList<User> GetAllUsers()
    {
        return _context.Users.Include(u => u.Rounds).ToList();
    }

    public User? GetUser(string? username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }

    public User AddUser(User user)
    {
        // If user already exists, throw exception
        if (_context.Users.Any(u => u.Username == user.Username))
            throw new Exception("User already exists");
        var userDb = _context.Users.Add(user);
        _context.SaveChanges();
        return userDb.Entity;
    }
}