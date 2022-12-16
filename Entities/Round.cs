using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeForARound.Entities;

public class Round
{
    [Key]
    public Guid Id { get; set; }
    
    public string? Username { get; set; }
    public User? User { get; set; }

    [Required] public string Reason { get; set; } = string.Empty;
    [Required] public DateTime OccurredAt { get; set; }
    [Required] public DateTime ReportedAt { get; set; }
    
    [Required] public bool AsBeenPaid { get; set; }
}