using Recipe_web_rest.Data;
using Recipe_web_rest.Interfaces;
using Recipe_web_rest.Models;

namespace Recipe_web_rest.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public ICollection<Review> GetReivewsbyRecipeId(int recipeId)
        {
            return _context.Reviews.Where(r => r.RecipeId == recipeId).OrderByDescending(r => r.Date).ToList();
        }

        public ICollection<Review> GetReivewsbyRecipeId_userId(int recipeId, int userId)
        {
            return _context.Reviews.Where(r => r.RecipeId == recipeId && r.UserId == userId).OrderByDescending(r => r.Date).ToList();
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
