using CricketStats.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketStats.Application.Interface
{
    public interface IPlayerRepository
    {
        List<Player> GetAll();
        Player? GetById(int id);
        Player Add(Player player);
        Player? Update(Player player);
        bool Delete(int id);
    }
}
