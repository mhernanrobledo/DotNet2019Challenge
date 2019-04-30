using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System;
using DotNet2019Challenge.Models;
using System.IO;

namespace DotNet2019Challenge.Services.Movies
{
    public class MoviesService: IMoviesService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly JsonSerializer _serializer;

        public MoviesService()
        {
            _serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializer.Converters.Add(new StringEnumConverter());
        }

        public async Task<SearchResponse<Movie>> GetPopularMoviesAsync(int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}movie/popular?api_key={AppSettings.ApiKey}&language={language}&page={pageNumber}";

            var response = await _httpClient.GetAsync(uri);

            await HandleResponse(response);

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                return _serializer.Deserialize<SearchResponse<Movie>>(json);
            }
        }

        public async Task<SearchResponse<Movie>> GetTopRatedMoviesAsync(int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}movie/top_rated?api_key={AppSettings.ApiKey}&language={language}&page={pageNumber}";

            var response = await _httpClient.GetAsync(uri);

            await HandleResponse(response);

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                return _serializer.Deserialize<SearchResponse<Movie>>(json);
            }
        }
        public async Task<SearchResponse<TVShow>> GetPopularShowsAsync(int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}tv/popular?api_key={AppSettings.ApiKey}&language={language}&page={pageNumber}";

            var response = await _httpClient.GetAsync(uri);

            await HandleResponse(response);

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                return _serializer.Deserialize<SearchResponse<TVShow>>(json);
            }
        }

        public async Task<SearchResponse<TVShow>> GetTopRatedShowsAsync(int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}tv/top_rated?api_key={AppSettings.ApiKey}&language={language}&page={pageNumber}";

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            await HandleResponse(response);

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                return _serializer.Deserialize<SearchResponse<TVShow>>(json);
            }
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }

                throw new HttpRequestException(content);
            }
        }

    }
}