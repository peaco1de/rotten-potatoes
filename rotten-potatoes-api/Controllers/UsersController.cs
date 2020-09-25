using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using rotten_potatoes_api.Models;

namespace rotten_potatoes_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private const string IGDB_KEY = "236c3817b529d90e52ffddbc60c8b0d3";
        private const string IGDB_URL = @"https://api-v3.igdb.com/games";

        private readonly HttpClient _client = new HttpClient();
        private readonly ReviewsContext _context = new ReviewsContext();

        [HttpGet()]
        public IActionResult GetUsers()
        {
            return new JsonResult(_context.Users.Select(o => new
            {
                o.UserId,
                o.UserName
            }));
        }

        [HttpGet("{userId}/reviews")]
        public IActionResult GetReviews(int userId)
        {
            var result = new List<Review>();

            result.AddRange(_context.Reviews.Where(o => o.UserId == userId).Select(o =>
            new Review
            {
                ReviewId = o.ReviewId,
                GameId = o.GameId,
                UserId = o.UserId,
                Score = o.Score,
                Details = o.Details,
                AddDate = o.AddDate
            }));

            return new JsonResult(result);
        }

        [HttpGet("{userId}/reviews/{gameId}")]
        public IActionResult GetReview(int userId, int gameId)
        {
            var review = _context.Reviews.SingleOrDefault(o => o.GameId == gameId && o.UserId == userId);
            if (review != null)
            {
                return new JsonResult(new
                {
                    review.GameId,
                    review.UserId,
                    review.Score,
                    review.Details,
                    review.AddDate
                });
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost()]
        public IActionResult PostUser([FromBody] PostUser args)
        {
            if (_context.Users.Any(o => o.UserName == args.UserName))
            {
                return null;
            }
            else
            {
                var newUser = new User { UserName = args.UserName };
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return new JsonResult(new { newUser.UserId, newUser.UserName });
            }
        }

        [HttpGet("{userId}/games")]
        public IActionResult GetFavoriteGames(int userId, string search = null)
        {
            List<Game> games;

            var favorites = _context.Users.Single(o => o.UserId == userId).Favorites.Select(o => o.GameId);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, IGDB_URL))
            {
                var builder = new StringBuilder();
                builder.Append("fields id, name, summary, cover.url; ");
                builder.Append($"where themes != (42) & cover.url != null & id = ({ string.Join(',', favorites) })");
                if (search != null) { builder.Append($" & name ~ *\"{ search }\"*"); };
                builder.Append(";");

                requestMessage.Headers.Add("user-key", IGDB_KEY);
                requestMessage.Content = new StringContent(builder.ToString());
                var responseTask = _client.SendAsync(requestMessage);
                games = JsonSerializer.Deserialize<List<Game>>(responseTask.Result.Content.ReadAsStringAsync().Result);
            }

            var scores = _context.Reviews.GroupBy(o => o.GameId).Select(o => new { Id = o.Key, AvgScore = o.Average(g => g.Score), NumberOfReviews = o.Count() }).ToList();

            var result =
                from game in games
                join score in scores
                on game.Id equals score.Id into g
                from s in g.DefaultIfEmpty()
                select new
                {
                    game.Id,
                    game.Name,
                    AvgScore = (s != null ? (double?)s.AvgScore : null),
                    NumberOfReviews = (s != null ? (int?)s.NumberOfReviews : 0),
                    CoverUrl = game.Cover.Url,
                    game.Summary,
                    isFavorite = true
                };

            return new JsonResult(result);
        }

        [HttpGet("{userId}/favorites")]
        public IActionResult GetFavorites(int userId)
        {
            return new JsonResult(_context.Users.Single(o => o.UserId == userId).Favorites.Select(o => o.GameId));
        }

        [HttpPost("{userId}/favorites")]
        public IActionResult PutFavorite(int userId, [FromBody] PutFavorite args)
        {
            if (_context.Favorites.Any(o => o.GameId == args.GameId && o.UserId == userId))
            {
                return Ok();
            }
            else
            {
                var newFavorite = new Favorite { UserId = userId, GameId = args.GameId };
                _context.Favorites.Add(newFavorite);
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete("{userId}/favorites/{gameId}")]
        public IActionResult DeleteFavorite(int userId, int gameId)
        {
            var favorite = _context.Favorites.SingleOrDefault(o => o.GameId == gameId && o.UserId == userId);

            if (favorite == null)
            {
                return Ok();
            }
            else
            {
                _context.Favorites.Remove(favorite);
                _context.SaveChanges();
                return Ok();
            }
        }

    }
}