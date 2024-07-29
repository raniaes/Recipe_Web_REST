﻿using AutoMapper;
using Recipe_web_rest.Dto;
using Recipe_web_rest.Models;

namespace Recipe_web_rest.Helper
{
    public class Mappingprofiles : Profile
    {
        public Mappingprofiles() 
        {
            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeDto, Recipe>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<IngredientDto, Ingredient>();
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
        }
    }
}
