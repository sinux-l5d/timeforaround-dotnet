namespace TimeForARound.Dto;

public class RoundDto
{
    public Guid Id { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime OccurredAt { get; set; }
    public DateTime ReportedAt { get; set; }
    
    public bool AsBeenPaid { get; set; }
}