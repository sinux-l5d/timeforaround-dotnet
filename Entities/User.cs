using System.ComponentModel.DataAnnotations;

namespace TimeForARound.Entities;

public class User
{
    [Key] public int Id { get; set; }
    
    [Required] public string Username { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Required] public DateTime JoinDate { get; set; }
    
    public IList<Round> Rounds { get; set; } = new List<Round>();
}