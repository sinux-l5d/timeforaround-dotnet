namespace TimeForARound.Dto;

public class UserDto
{
    public string Username { get; set; } = string.Empty;
    public IList<RoundDto>? Rounds { get; set; }
}