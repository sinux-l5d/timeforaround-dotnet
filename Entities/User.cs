using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TimeForARound.Entities;

public class User
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string? Username { get; set; }
    [Required] public DateTime JoinDate { get; set; }
    
    public List<Round>? Rounds { get; set; }
}