using System.ComponentModel.DataAnnotations;

namespace TimeForARound.Dto;

public class RoundNewDto
{
    [Required]
    public string? Reason { get; set; }
    
    [Required]
    public DateTime OccuredAt { get; set; }

    public bool AsBeenPaid { get; set; } = false;
}