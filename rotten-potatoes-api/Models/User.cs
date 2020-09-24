using System;
using System.Collections;
using System.Collections.Generic;

namespace rotten_potatoes_api.Models
{
    public class User
    {
        public string UserName { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}