using Recipe_web_rest.Models;

namespace Recipe_web_rest.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection <Recipe> GetRecipes ();
        Recipe GetRecipe (int id);
        bool RecipeExists (int id);
        bool CreateRecipe (int[] ingredientId, Recipe recipe);
        bool UpdateRecipe (int[] ingredientId, Recipe recipe);
        bool DeleteRecipe (Recipe recipe);
        bool Save();
    }
}
