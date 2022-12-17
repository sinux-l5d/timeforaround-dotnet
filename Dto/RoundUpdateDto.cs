using System.ComponentModel.DataAnnotations;

namespace TimeForARound.Dto;

public class RoundUpdateDto
{
    [Required]
    public bool AsBeenPaid { get; set; }
}