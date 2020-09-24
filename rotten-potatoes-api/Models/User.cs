using System;
using System.Collections;
using System.Collections.Generic;

namespace rotten_potatoes_api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}