using System.Collections.Generic;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public interface IGameRepository : IRepositoryBase<Game>
    {
        IEnumerable<Game> GetHighestRatedGames(int numberOfGamesToReceive);
        Game GetRandomGame();
    }
}