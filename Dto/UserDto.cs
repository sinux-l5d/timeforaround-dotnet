namespace TimeForARound.Dto;

public class UserDto
{
    public string Username { get; set; } = string.Empty;
    public int RoundsCount { get; set; }
    public int RoundsPaid { get; set; }
}