using CricketStats.Application.Interface;
using CricketStats.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketStats.Infrastructure.Service
{
    public class InMemoryPlayerRepository : IPlayerRepository
    {
        private readonly List<Player> _players = new();

        public List<Player> GetAll() => _players;

        public Player? GetById(int id) =>
        _players.FirstOrDefault(x => x.Id == id);

        public Player Add(Player player)
        {
            player.Id = _players.Count + 1;
            _players.Add(player);
            return player;
        }

        public Player Update(Player player)
        {
            var exisiting = _players.Where(x => x.Id == player.Id).FirstOrDefault();
            if (exisiting != null)
            {
                exisiting.Id = player.Id;
                exisiting.Name = player.Name;
                exisiting.Country = player.Country;
                exisiting.Matches = player.Matches;
                exisiting.Runs = player.Runs;
            }

            return exisiting!;
        }
        public bool Delete(int id)
        {
            var player = _players.FirstOrDefault(x => x.Id == id);
            if (player == null) return false;

            _players.Remove(player);
            return true;
        }
    }
}
