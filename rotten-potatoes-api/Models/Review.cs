using System;

namespace rotten_potatoes_api.Models
{
    public class Review
    {
        public int Game { get; set; }
        public string User { get; set; }
        public int Score { get; set; }
        public string Details { get; set; }
        public DateTime AddDate { get; set; }
    }
}