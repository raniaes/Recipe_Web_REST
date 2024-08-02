namespace Recipe_web_rest.Models
{
    public class Review
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
