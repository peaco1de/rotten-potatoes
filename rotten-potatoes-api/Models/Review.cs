namespace rotten_potatoes_api.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int Game { get; set; }
        public int Score { get; set; }
        public string Details { get; set; }
        public string User { get; set; }
    }
}