using Recipe_web_rest.Models;

namespace Recipe_web_rest.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool Save();
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
    }
}
