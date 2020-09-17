using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
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
        private const string IGDB = @"https://api-v3.igdb.com/games";

        private static readonly HttpClient _client = new HttpClient();
        private static readonly ReviewsContext _context = new ReviewsContext();

        public ReviewsController()
        {

        }

        [HttpGet("games/search/{search}")]
        public IActionResult GetGames(string search)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, IGDB))
            {
                requestMessage.Headers.Add("user-key", IGDB_KEY);
                requestMessage.Content = new StringContent($"fields id, name; limit 100; search \"{search}\";");
                var responseTask = _client.SendAsync(requestMessage);

                return Content(responseTask.Result.Content.ReadAsStringAsync().Result);
            }
        }

        [HttpGet("scores")]
        public IActionResult GetScores()
        {
            List<dynamic> games;

            var ids = _context.Reviews.Select(o => o.Game);
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, IGDB))
            {
                requestMessage.Headers.Add("user-key", IGDB_KEY);
                requestMessage.Content = new StringContent($"fields id, name; limit 100; where id = ({string.Join(',', ids)});");
                var responseTask = _client.SendAsync(requestMessage);
                games = JsonSerializer.Deserialize<List<dynamic>>(responseTask.Result.Content.ReadAsStringAsync().Result);
            }

            var scores = _context.Reviews.GroupBy(o => o.Game).Select(o => new { GameId = o.Key, AvgScore = o.Average(g => g.Score), NumberOfReviews = o.Count() }).ToList();

            var result = scores.Join(games, o => o.GameId, i => i.id, (o, i) => new { o.GameId, Name = i.name, o.AvgScore, o.NumberOfReviews });

            return new JsonResult(result);
        }

        [HttpGet("games/{id}")]
        public IActionResult GetGame(int id)
        {
            dynamic game;

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, IGDB))
            {
                requestMessage.Headers.Add("user-key", IGDB_KEY);
                requestMessage.Content = new StringContent($"fields id, name; where id = {id};");
                var responseTask = _client.SendAsync(requestMessage);
                game = JsonSerializer.Deserialize<List<dynamic>>(responseTask.Result.Content.ReadAsStringAsync().Result).FirstOrDefault();
            }

            return new JsonResult(game);
        }

        [HttpGet("reviews/{gameId}")]
        public IActionResult GetReviews(int gameId)
        {
            return new JsonResult(_context.Reviews.Where(o => o.Game == gameId));
        }

        [HttpPut("reviews/{gameId}/{user}")]
        public IActionResult EditReview([FromBody] EditReview args)
        {
            var review = _context.Reviews.SingleOrDefault(o => o.Game == args.Game && o.User == args.User);

            if (review == null)
            {
                _context.Reviews.Add(
                    new Review
                    {
                        Game = args.Game,
                        User = args.User,
                        Score = args.Score,
                        Details = args.Details
                    }
                );

                _context.SaveChanges();
                return Ok("Added");
            }
            else
            {
                review.Score = args.Score;
                review.Details = args.Details;

                _context.SaveChanges();
                return Ok("Edited");
            }
        }

        [HttpDelete("reviews")]
        public IActionResult DeleteReview([FromBody] DeleteReview args)
        {
            var review = _context.Reviews.SingleOrDefault(o => o.Game == args.Game && o.User == args.User);
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
