using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace rotten_potatoes_api.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public string Details { get; set; }
        public DateTime AddDate { get; set; }
    }
}