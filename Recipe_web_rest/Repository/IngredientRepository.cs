using Recipe_web_rest.Data;
using Recipe_web_rest.Interfaces;
using Recipe_web_rest.Models;

namespace Recipe_web_rest.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly DataContext _context;

        public IngredientRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateIngredient(Ingredient ingredient)
        {
            _context.Add(ingredient);
            return Save();
        }

        public bool DeleteIngredient(Ingredient ingredient)
        {
            _context.Remove(ingredient);
            return Save();
        }

        public Ingredient GetIngredient(int id)
        {
            return _context.Ingredients.Where(I => I.Id == id).FirstOrDefault();
        }

        public ICollection<Ingredient> GetIngredients()
        {
            return _context.Ingredients.ToList();
        }

        public bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(I =>  I.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateIngredient(Ingredient ingredient)
        {
            _context.Update(ingredient);
            return Save();
        }
    }
}
