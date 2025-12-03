namespace CricketStats.Domain.Entities;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int Matches { get; set; }
    public int Runs { get; set; }
}
