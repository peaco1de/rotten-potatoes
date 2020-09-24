using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
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
        private readonly ReviewsContext _context = new ReviewsContext();

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return new JsonResult(_context.Users.Select(o => new
            {
                o.UserId,
                o.UserName
            }));
        }

        [HttpGet("users/{userId}/reviews/{gameId}")]
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

        [HttpGet("users/{userId}/reviews")]
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

        [HttpPost("users")]
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
    }
}