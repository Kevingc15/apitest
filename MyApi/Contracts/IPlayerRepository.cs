using MyApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi.Contracts
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAll();
        Task<Player> GetById(int id);
        Task Insert(Player player);
        Task DeleteById(int id);
    }
}
