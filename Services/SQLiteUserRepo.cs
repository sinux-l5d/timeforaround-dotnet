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
        if (username == null) return null;
        return _context.Users
            .Include(u => u.Rounds)
            .FirstOrDefault(u => u.Username == username);
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

    public IList<Round> GetAllRounds()
    {
        return _context.Rounds.ToList();
    }

    public Round AddRound(Round round)
    {
        var username = round.Username;
        
        if (!_context.Users.Any(u => u.Username == username))
            throw new Exception("User does not exist");
        
        round.Id = Guid.NewGuid(); 
        var roundDb = _context.Rounds.Add(round);
        _context.SaveChanges();
        
        return roundDb.Entity;
    }
    
    public Round SetPaid(Guid roundId, bool paid)
    {
        var round = _context.Rounds.FirstOrDefault(r => r.Id == roundId);
        if (round == null)
            throw new Exception("Round does not exist");
        round.AsBeenPaid = paid;
        _context.SaveChanges();
        return round;
    }
}