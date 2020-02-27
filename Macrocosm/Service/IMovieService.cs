using Macrocosm.Models;
using Macrocosm.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Service
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMoviesAsync(MovieParameter parameter);
        Task<RedisViewModel> GetRedisModelAsync();
        Task<Movie> ViewAsync(int id);
        Task<System_User> LoginVerification(string username, string password);
    }
}
