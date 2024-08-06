using AutoMapper;
using Recipe_web_rest.Dto;
using Recipe_web_rest.Models;
using Recipe_web_rest.Request;

namespace Recipe_web_rest.Helper
{
    public class Mappingprofiles : Profile
    {
        public Mappingprofiles() 
        {
            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeDto, Recipe>();
            CreateMap<Recipe, RecipeList>();
            CreateMap<RecipeList, Recipe>();
            CreateMap<Recipe, RecipeCreate>();
            CreateMap<RecipeCreate, Recipe>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<IngredientDto, Ingredient>();
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();

            CreateMap<Recipe, RecipeImageDto>();
            CreateMap<RecipeImageDto, Recipe>();
        }
    }
}
