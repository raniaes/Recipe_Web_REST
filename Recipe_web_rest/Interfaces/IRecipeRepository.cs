using Recipe_web_rest.Models;

namespace Recipe_web_rest.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection <Recipe> GetRecipes ();
        ICollection<Recipe> GetSearch (string word);
        ICollection<Recipe> GetFilter(string categoryName);
        ICollection<Recipe> GetFilter_Searcch(string categoryName, string word);
        Recipe GetRecipe (int id);
        bool RecipeExists (int id);
        bool CreateRecipe (int[] ingredientId, Recipe recipe);
        bool UpdateRecipe (int[] ingredientId, Recipe recipe);
        bool DeleteRecipe (Recipe recipe);
        bool Save();
    }
}
