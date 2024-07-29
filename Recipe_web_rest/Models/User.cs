namespace Recipe_web_rest.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
