namespace TimeForARound.Dto;

public class UserDetailedDto
{
    public string Username { get; set; } = string.Empty;
    public IList<RoundDto> Rounds { get; set; } = new List<RoundDto>();
}