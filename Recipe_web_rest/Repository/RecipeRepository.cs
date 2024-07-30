using Microsoft.EntityFrameworkCore;
using Recipe_web_rest.Data;
using Recipe_web_rest.Interfaces;
using Recipe_web_rest.Models;

namespace Recipe_web_rest.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataContext _dataContext;
        public RecipeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateRecipe(int[] ingredientId, Recipe recipe)
        {
            var ingredients = _dataContext.Ingredients.Where(i => ingredientId.Contains(i.Id)).ToList();
            

            foreach (var ingredient in ingredients)
            {
                var recipe_ingredient = new Recipe_Ingredient() 
                { 
                    Recipe = recipe,
                    Ingredient = ingredient
                };
                _dataContext.Add(recipe_ingredient);
            }

            _dataContext.Add(recipe);
            return Save();
        }

        public bool DeleteRecipe(Recipe recipe)
        {
            _dataContext.Remove(recipe);
            return Save();
        }

        public ICollection<Recipe> GetFilter(string categoryname)
        {
            return _dataContext.Recipes.Where(r => r.Category.Name == categoryname).ToList();
        }

        public ICollection<Recipe> GetFilter_Searcch(string categoryName, string word)
        {
            return _dataContext.Recipes
                .Where(r => r.Category.Name == categoryName && EF.Functions.Like(r.Name, $"%{word}%"))
                .ToList();
        }

        public Recipe GetRecipe(int id)
        {
            return _dataContext.Recipes.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Recipe> GetRecipes()
        {
            return _dataContext.Recipes.ToList();
        }

        public ICollection<Recipe> GetSearch(string word)
        {
            return _dataContext.Recipes.Where(r => EF.Functions.Like(r.Name, $"%{word}%")).ToList();
        }

        public bool RecipeExists(int id)
        {
            return _dataContext.Recipes.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRecipe(int[] ingredientId, Recipe recipe)
        {
            var exRE_INGr = _dataContext.Recipe_Ingredients.Where(ri => ri.RecipeId == recipe.Id).ToList();
            _dataContext.Recipe_Ingredients.RemoveRange(exRE_INGr);

            var ingredients = _dataContext.Ingredients.Where(i => ingredientId.Contains(i.Id)).ToList();


            foreach (var ingredient in ingredients)
            {
                var recipe_ingredient = new Recipe_Ingredient()
                {
                    Recipe = recipe,
                    Ingredient = ingredient
                };
                _dataContext.Add(recipe_ingredient);
            }


            _dataContext.Update(recipe);
            return Save();
        }
    }
}
