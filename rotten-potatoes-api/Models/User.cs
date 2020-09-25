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

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}