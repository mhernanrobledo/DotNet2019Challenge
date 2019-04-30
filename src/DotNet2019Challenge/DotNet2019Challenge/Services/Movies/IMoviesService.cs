using System.Threading.Tasks;
using DotNet2019Challenge.Models;

namespace DotNet2019Challenge.Services.Movies
{
    public interface IMoviesService
    {
        Task<SearchResponse<Movie>> GetPopularMoviesAsync(int pageNumber = 1, string language = "en");
        Task<SearchResponse<TVShow>> GetPopularShowsAsync(int pageNumber = 1, string language = "en");
        Task<SearchResponse<Movie>> GetTopRatedMoviesAsync(int pageNumber = 1, string language = "en");
        Task<SearchResponse<TVShow>> GetTopRatedShowsAsync(int pageNumber = 1, string language = "en");

    }
}
