using System.Collections.Generic;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public interface IGameRepository : IRepositoryBase<Game>
    {
        List<Game> GetHighestRatedGames();
        Game GetRandomGame();
    }
}