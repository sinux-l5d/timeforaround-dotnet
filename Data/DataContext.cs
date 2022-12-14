using Microsoft.EntityFrameworkCore;

namespace TimeForARound.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Entities.User> Users { get; set; } = null!;
    public DbSet<Entities.Round> Rounds { get; set; } = null!;
}