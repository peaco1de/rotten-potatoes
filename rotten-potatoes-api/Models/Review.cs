using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace rotten_potatoes_api.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int Score { get; set; }
        public string Details { get; set; }
        public virtual DateTime AddDate { get; set; }
    }
}