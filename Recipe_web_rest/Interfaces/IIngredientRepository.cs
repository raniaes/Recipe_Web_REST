using Recipe_web_rest.Models;

namespace Recipe_web_rest.Interfaces
{
    public interface IIngredientRepository
    {
        ICollection<Ingredient> GetIngredients();
        Ingredient GetIngredient(int id);
        bool IngredientExists(int id);
        bool CreateIngredient(Ingredient ingredient);
        bool Save();
        bool UpdateIngredient(Ingredient ingredient);
        bool DeleteIngredient(Ingredient ingredient);
    }
}
