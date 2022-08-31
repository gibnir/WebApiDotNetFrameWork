using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiHttpClient.Models;

namespace WebApiHttpClient.Controllers
{
    [Route("Movie")]
    public class MovieController : ApiController
    {
        private static HttpClient _httpClient;

        static MovieController() {
            _httpClient = new HttpClient {
                BaseAddress = new Uri("http://www.omdbapi.com/")
            };
        }

        public MovieController() {

        }

        #region HttpGets return json as string

        [HttpGet]
        //[System.Web.Mvc.ValidateInput(false)]
        [Route("GetAllMovies/{SearchText}")]
        public async Task<string> GetAllMovies(string SearchText)
        {

            // var URL = $"http://www.omdbapi.com/?apikey=yourapikey={SearchText}";
            var URL = $"{_httpClient.BaseAddress}?apikey=yourapikey={SearchText}";

            // var httpClient = new HttpClient();

            // var httpClient = _httpClientFactory.CreateClient("Movie");

            var response = _httpClient.GetAsync(URL).Result;

            // response.Content.ReadAsStringAsync().Wait();

            return await response.Content.ReadAsStringAsync();
        }

        [HttpGet]
        [Route("GetMovie/{id}")]
        public async Task<string> GetMovie(string id)
        {

            var URL = $"{_httpClient.BaseAddress}?apikey=ce974a95&i={id}";

            // var httpClient = new HttpClient();

            // var httpClient = _httpClientFactory.CreateClient("Movie");

            var response = _httpClient.GetAsync(URL).Result;

            // response.Content.ReadAsStringAsync().Wait();

            return await response.Content.ReadAsStringAsync();
        }

        #endregion HttpGets return json as string

        #region HttpGets return json as List<T>

        [HttpGet]
        [Route("GetAll/{SearchText}")]
        public async Task<List<Movie>> GetAll(string SearchText)
        {

            List<Movie> movies = new List<Movie>();

            Root root = new Root();

            // var URL = $"http://www.omdbapi.com/?apikey=yourapikey={SearchText}";
            var URL = $"{_httpClient.BaseAddress}?apikey=yourapikey={SearchText}";

            // var httpClient = new HttpClient();

            // var httpClient = _httpClientFactory.CreateClient("Movie");

            var response = _httpClient.GetAsync(URL).Result;

            string data = await response.Content.ReadAsStringAsync();
            // movies = await response.Content.ReadFromJsonAsync<Movie[]>();


#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            root = JsonConvert.DeserializeObject<Root>(data);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            movies = root.Search;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            // return JsonConvert.SerializeObject(movies);
#pragma warning disable CS8603 // Possible null reference return.
            return movies;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpGet]
        [Route("GetMovieById/{id}")]
        public async Task<MovieDetails> GetMovieById(string id)
        {

            MovieDetails movieDetails = new MovieDetails();

            var URL = $"{_httpClient.BaseAddress}?apikey=ce974a95&i={id}";
            // var httpClient = _httpClient.CreateClient("Movie");
            var response = _httpClient.GetAsync(URL).Result;

            string data = await response.Content.ReadAsStringAsync();



#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            movieDetails = JsonConvert.DeserializeObject<MovieDetails>(data);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            // return JsonConvert.SerializeObject(movies);
#pragma warning disable CS8603 // Possible null reference return.
            return movieDetails;
#pragma warning restore CS8603 // Possible null reference return.
        }

        #endregion HttpGets return json as List<T>

        // GET: api/Movie
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Movie/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Movie
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Movie/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Movie/5
        public void Delete(int id)
        {
        }
    }
}
