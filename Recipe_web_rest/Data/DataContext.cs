using Microsoft.EntityFrameworkCore;
using Recipe_web_rest.Models;

namespace Recipe_web_rest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Recipe_Ingredient> Recipe_Ingredients { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe_Ingredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });
            modelBuilder.Entity<Recipe_Ingredient>()
                .HasOne(r => r.Recipe)
                .WithMany(ri => ri.Recipe_Ingredients)
                .HasForeignKey(r => r.RecipeId);
            modelBuilder.Entity<Recipe_Ingredient>()
                .HasOne(i => i.Ingredient)
                .WithMany(ri => ri.Recipe_Ingredients)
                .HasForeignKey(i => i.IngredientId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Recipe)
                .WithMany(rec => rec.Reviews)
                .HasForeignKey(r => r.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
