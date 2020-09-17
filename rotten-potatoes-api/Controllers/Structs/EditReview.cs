using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rotten_potatoes_api.Controllers
{
    public struct EditReview
    {
        public int Game;
        public string User;
        public int Score;
        public string Details;
    }
}
