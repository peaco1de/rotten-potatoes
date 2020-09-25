using rotten_potatoes_api.Controllers;

namespace rotten_potatoes_api.Models
{
    public class Favorite
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int GameId { get; set; }
    }
}