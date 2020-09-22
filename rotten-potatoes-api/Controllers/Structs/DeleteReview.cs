using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rotten_potatoes_api.Controllers
{
    public struct DeleteReview
    {
        public int Game { get; set; }
        public string User { get; set; }
    }
}
