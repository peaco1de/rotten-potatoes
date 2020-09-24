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
using Microsoft.Extensions.Logging;
using rotten_potatoes_api.Models;

namespace rotten_potatoes_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly ReviewsContext _context = new ReviewsContext();

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return new JsonResult(_context.Users);
        }

        [HttpGet("users/{userName}/reviews")]
        public IActionResult GetReviews(int userId)
        {
            return new JsonResult(_context.Users.SingleOrDefault(o => o.Id == userId)?.Reviews);
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
                return new JsonResult(newUser);
            }
        }
    }
}