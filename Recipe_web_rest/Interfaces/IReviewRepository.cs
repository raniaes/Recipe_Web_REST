using Recipe_web_rest.Models;

namespace Recipe_web_rest.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        ICollection<Review> GetReivewsbyRecipeId(int recipeId);
        ICollection<Review> GetReivewsbyRecipeId_userId(int recipeId, int userId);
        Review GetReview(int id);
        bool ReviewExists(int id);
        bool CreateReview(Review review);
        bool Save();
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
    }
}
