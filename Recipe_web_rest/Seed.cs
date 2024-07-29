using Recipe_web_rest.Data;
using Recipe_web_rest.Models;

namespace Recipe_web_rest
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.Recipes.Any())
            {
                var review = new Review()
                {
                    Title = "good rice", Content = "really taste", Rating = 5
                };

                var recipes = new List<Recipe>()
                {
                    new Recipe()
                    {
                        Name = "Rice",
                        Instruction = "wash rice, using cooker",
                        Pic_address = "c//..",
                        User = new User
                        {
                            UserId = "Mok",
                            Password = BCrypt.Net.BCrypt.HashPassword("test123"),
                            Reviews = new List<Review>() {review},
                        },
                        Category = new Category() { Name = "Dinner"},
                        Reviews = new List<Review>() {review},
                        Recipe_Ingredients = new List<Recipe_Ingredient>()
                        {
                            new Recipe_Ingredient { Ingredient = new Ingredient { Name = "Rice"}},
                            new Recipe_Ingredient { Ingredient = new Ingredient { Name = "Water"}}
                        }
                    }
                };

                review.User = recipes[0].User;
                review.Recipe = recipes[0];

                dataContext.Recipes.AddRange(recipes);
                dataContext.SaveChanges();
            }
        }
    }
}
