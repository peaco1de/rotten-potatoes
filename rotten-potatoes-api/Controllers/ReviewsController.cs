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
        private readonly HttpClient _client = new HttpClient();
        private readonly ReviewsContext _context = new ReviewsContext();

        public ReviewsController()
        {

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
                return Ok();
            }
            else
            {
                return NotFound();
            };
        }
    }
}
