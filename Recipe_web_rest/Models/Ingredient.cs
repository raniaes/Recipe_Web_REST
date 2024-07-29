namespace Recipe_web_rest.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Recipe_Ingredient> Recipe_Ingredients { get; set;}
    }
}
