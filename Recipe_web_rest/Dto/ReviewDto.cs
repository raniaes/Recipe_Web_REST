namespace Recipe_web_rest.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
    }
}
