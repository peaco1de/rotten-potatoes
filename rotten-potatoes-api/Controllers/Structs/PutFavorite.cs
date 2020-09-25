using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rotten_potatoes_api.Controllers
{
    public struct EditReview
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public string Details { get; set; }
    }
}
