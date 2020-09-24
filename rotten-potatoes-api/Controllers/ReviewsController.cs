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
using Microsoft.Extensions.Logging;
using rotten_potatoes_api.Models;

namespace rotten_potatoes_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private const string IGDB_KEY = "236c3817b529d90e52ffddbc60c8b0d3";
        private const string IGDB_URL = @"https://api-v3.igdb.com/games";

        private static readonly HttpClient _client = new HttpClient();
        private static readonly ReviewsContext _context = new ReviewsContext();

        public ReviewsController()
        {

        }

        //[HttpGet("games")]
        //public IActionResult GetGames()
        //{
        //    List<Game> games;

        //    using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, IGDB_URL))
        //    {
        //        requestMessage.Headers.Add("user-key", IGDB_KEY);
        //        requestMessage.Content = new StringContent($"fields id, name, summary, cover.url; limit 100; where themes != (42) & cover.url != null;");
        //        var responseTask = _client.SendAsync(requestMessage);
        //        games = JsonSerializer.Deserialize<List<Game>>(responseTask.Result.Content.ReadAsStringAsync().Result);
        //    }

        //    var scores = _context.Reviews.GroupBy(o => o.GameId).Select(o => new { Id = o.Key, AvgScore = o.Average(g => g.Score), NumberOfReviews = o.Count() }).ToList();

        //    var result =
        //        from game in games
        //        join score in scores
        //        on game.Id equals score.Id into g
        //        from s in g.DefaultIfEmpty()
        //        select new
        //        {
        //            game.Id,
        //            game.Name,
        //            AvgScore = (s != null ? (double?)s.AvgScore : null),
        //            NumberOfReviews = (s != null ? (int?)s.NumberOfReviews : 0),
        //            CoverUrl = game.Cover.Url,
        //            game.Summary
        //        };

        //    return new JsonResult(result);
        //}
        
        [HttpGet("games")]
        public IActionResult GetGames(string search = null, int? gameId = null)
        {
            using ReviewsContext context = new ReviewsContext();

            List<Game> games;

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, IGDB_URL))
            {
                var builder = new StringBuilder();
                builder.Append("fields id, name, summary, cover.url; limit 100; ");
                builder.Append("where themes != (42) & cover.url != null");
                if (search != null) { builder.Append($" & name ~ *\"{ search }\"*"); }
                if (gameId != null) { builder.Append($" & id = { gameId }"); }
                builder.Append(";");

                Console.WriteLine(gameId);

                requestMessage.Headers.Add("user-key", IGDB_KEY);
                requestMessage.Content = new StringContent(builder.ToString());
                var responseTask = _client.SendAsync(requestMessage);
                games = JsonSerializer.Deserialize<List<Game>>(responseTask.Result.Content.ReadAsStringAsync().Result);
            }

            var scores = context.Reviews.GroupBy(o => o.GameId).Select(o => new { Id = o.Key, AvgScore = o.Average(g => g.Score), NumberOfReviews = o.Count() }).ToList();

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
                    game.Summary
                };

            return new JsonResult(result);
        }


        //should be games/id/reviews
        [HttpGet("reviews/{gameId}")]
        public IActionResult GetReviews(int gameId)
        {
            return new JsonResult(_context.Reviews.Where(o => o.GameId == gameId).Select(o =>
            new {
                o.GameId,
                o.UserId,
                o.Score,
                o.Details,
                o.AddDate
            }));
        }

        [HttpPut("reviews")]
        public IActionResult EditReview([FromBody] EditReview args)
        {
            var review = _context.Reviews.SingleOrDefault(o => o.GameId == args.GameId && o.UserId == args.UserId);
            if (review == null)
            {
                _context.Reviews.Add(
                    new Review
                    {
                        GameId = args.GameId,
                        UserId = args.UserId,
                        Score = args.Score,
                        Details = args.Details
                    }
                );

                _context.SaveChanges();
                return new JsonResult(new { Status = "Added", ScoreChange = 0 });
            }
            else
            {
                var oldScore = review.Score;
                review.Score = args.Score;
                review.Details = args.Details;

                _context.SaveChanges();
                return new JsonResult(new { Status = "Edited", ScoreChange = review.Score - oldScore });
            }
        }

        [HttpDelete("reviews/{reviewId}")]
        public IActionResult DeleteReview(int reviewId)
        {
            var review = _context.Reviews.SingleOrDefault(o => o.ReviewId == reviewId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
                return Ok("Deleted");
            }
            else
            {
                return NotFound();
            };
        }
    }
}
