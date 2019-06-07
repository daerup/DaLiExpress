using System.Collections.Generic;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public interface IGamesRepository : IRepositoryBase<Game>
    {
        IEnumerable<Game> GetHighestRatedGames(int numberOfGamesToReceive);
        Game GetRandomGame();
    }
}