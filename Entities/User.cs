using System.ComponentModel.DataAnnotations;

namespace TimeForARound.Entities;

public class User
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Username { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Required] public DateTime JoinDate { get; set; }
    
    public List<Round> Rounds { get; set; } = new();
}