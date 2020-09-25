using rotten_potatoes_api.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace rotten_potatoes_api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Favorite> Favorites { get; set; }
    }
}