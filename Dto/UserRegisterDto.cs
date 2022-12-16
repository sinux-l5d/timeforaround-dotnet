using System.ComponentModel.DataAnnotations;

namespace TimeForARound.Dto;

public class UserRegisterDto
{
    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }
}