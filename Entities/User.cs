using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TimeForARound.Entities;

public class User
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Username { get; set; } = string.Empty;
    [Required] public DateTime JoinDate { get; set; }

    public IList<Round> Rounds { get; set; } = new List<Round>();
}