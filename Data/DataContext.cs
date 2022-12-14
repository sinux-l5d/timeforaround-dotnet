using Microsoft.EntityFrameworkCore;
using TimeForARound.Entities;

namespace TimeForARound.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    /*
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Rounds)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId);
    }
    */

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Round> Rounds { get; set; } = null!;
}