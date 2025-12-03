using CricketStats.Application.Interface;
using CricketStats.Domain.Entities;

namespace CricketStats.Infrastructure.Service;

public class PlayerService : IPlayerService
{
    public Player GetSamplePlayer()
    {
        return new Player
        {
            Id = 1,
            Name = "Virat Kohli",
            Country = "India",
            Matches = 275,
            Runs = 13000
        };
    }
}
