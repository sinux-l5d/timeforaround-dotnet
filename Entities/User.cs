using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TimeForARound.Entities;

public class User
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string? Username { get; set; }
    [Required] public DateTime JoinDate { get; set; }
    
    public List<Round>? Rounds { get; set; }
}