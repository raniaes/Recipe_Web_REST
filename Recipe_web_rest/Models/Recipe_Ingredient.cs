namespace Recipe_web_rest.Models
{
    public class Recipe_Ingredient
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set;}
    }
}
