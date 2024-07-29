namespace Recipe_web_rest.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public string Pic_address { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Recipe_Ingredient> Recipe_Ingredients { get; set;}
    }
}
