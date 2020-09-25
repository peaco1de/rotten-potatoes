using Microsoft.AspNetCore.Mvc;
using rotten_potatoes_api.Migrations;
using rotten_potatoes_api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace rotten_potatoes_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private const string IGDB_KEY = "236c3817b529d90e52ffddbc60c8b0d3";
        private const string IGDB_URL = @"https://api-v3.igdb.com/games";

        private readonly HttpClient _client = new HttpClient();
        private readonly ReviewsContext _context = new ReviewsContext();

        [HttpGet()]
        public IActionResult GetGames(string search = null, int? gameId = null, int? userId = null)
        {
            List<Game> games;

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, IGDB_URL))
            {
                var builder = new StringBuilder();
                builder.Append("fields id, name, summary, cover.url; limit 100; ");
                builder.Append("where themes != (42) & cover.url != null");
                if (search != null) { builder.Append($" & name ~ *\"{ search }\"*"); }
                if (gameId != null) { builder.Append($" & id = { gameId }"); }
                builder.Append(";");

                requestMessage.Headers.Add("user-key", IGDB_KEY);
                requestMessage.Content = new StringContent(builder.ToString());
                var responseTask = _client.SendAsync(requestMessage);
                games = JsonSerializer.Deserialize<List<Game>>(responseTask.Result.Content.ReadAsStringAsync().Result);
            }

            var scores = _context.Reviews.GroupBy(o => o.GameId).Select(o => new { Id = o.Key, AvgScore = o.Average(g => g.Score), NumberOfReviews = o.Count() }).ToList();

            var favorites = _context.Users.SingleOrDefault(o => o.UserId == userId)?.Favorites?.Select(o => o.GameId) ?? new List<int>();

            var result =
                from game in games
                join score in scores
                on game.Id equals score.Id into g
                join favorite in favorites
                on game.Id equals favorite into f
                from t in f.DefaultIfEmpty(-1)
                from s in g.DefaultIfEmpty()
                select new
                {
                    game.Id,
                    game.Name,
                    AvgScore = (s != null ? (double?)s.AvgScore : null),
                    NumberOfReviews = (s != null ? (int?)s.NumberOfReviews : 0),
                    CoverUrl = game.Cover.Url,
                    game.Summary,
                    isFavorite = t != -1
                };

            return new JsonResult(result);
        }

        [HttpGet("{gameId}/reviews")]
        public IActionResult GetReviews(int gameId)
        {
            return new JsonResult(_context.Reviews.Where(o => o.GameId == gameId).Select(o =>
            new
            {
                o.ReviewId,
                o.GameId,
                o.UserId,
                o.Score,
                o.Details,
                o.AddDate
            }));
        }
    }
}