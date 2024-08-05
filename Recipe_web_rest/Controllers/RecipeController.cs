using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Recipe_web_rest.Data;
using Recipe_web_rest.Dto;
using Recipe_web_rest.Interfaces;
using Recipe_web_rest.Models;
using Recipe_web_rest.Repository;
using Recipe_web_rest.Request;

namespace Recipe_web_rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecipeController(IRecipeRepository recipeRepository, IUserRepository userRepository,
            ICategoryRepository categoryRepository, IMapper mapper, IWebHostEnvironment env)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _webHostEnvironment = env;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipes() 
        {
            var recipes = _mapper.Map<List<RecipeList>>(_recipeRepository.GetRecipes());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipes);
        }

        [HttpGet("{RecipeId}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipe(int RecipeId)
        {
            if (!_recipeRepository.RecipeExists(RecipeId))
            {
                return NotFound();
            }

            var recipe = _mapper.Map<RecipeDto>(_recipeRepository.GetRecipe(RecipeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipe);
        }

        [HttpGet("images/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "image/jpeg");
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRecipe([FromQuery] int userId, [FromQuery] int categoryId, [FromQuery] int[] ingreId, [FromBody] RecipeDto recipeCreate)
        {
            if (recipeCreate == null)
            {
                return BadRequest(ModelState);
            }

            var recipe = _recipeRepository.GetRecipes()
                .Where(c => c.Name.Trim().ToUpper() == recipeCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (recipe != null)
            {
                ModelState.AddModelError("", "Recipe already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipeMap = _mapper.Map<Recipe>(recipeCreate);

            recipeMap.UserId = userId;
            recipeMap.CategoryId = categoryId;

            if (!_recipeRepository.CreateRecipe(ingreId, recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int recipeId, [FromQuery] int[] ingreId, [FromQuery] int userId , [FromQuery] int categoryId, [FromBody] RecipeDto updateRecipe)
        {
            if (updateRecipe == null)
            {
                return BadRequest(ModelState);
            }

            if (recipeId != updateRecipe.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_recipeRepository.RecipeExists(recipeId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipeMap = _mapper.Map<Recipe>(updateRecipe);
            recipeMap.User = _userRepository.GetUser(userId);
            recipeMap.Category = _categoryRepository.GetCategory(categoryId);

            if (!_recipeRepository.UpdateRecipe(ingreId, recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating recipe");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteCategory(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
            {
                return NotFound();
            }

            var recipeToDelete = _recipeRepository.GetRecipe(recipeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pic_name = "";
            int lastindex = recipeToDelete.Pic_address.LastIndexOf('|');
            if (lastindex >= 0)
            {
                pic_name = recipeToDelete.Pic_address.Substring(lastindex + 1);
            }

            if (!string.IsNullOrEmpty(pic_name))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", pic_name);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            if (!_recipeRepository.DeleteRecipe(recipeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting recipe");
            }

            return NoContent();
        }

        [HttpGet("search/{searchname}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        [ProducesResponseType(400)]
        public IActionResult GetSearch(string searchname)
        {
            var recipes = _mapper.Map<List<RecipeList>>(_recipeRepository.GetSearch(searchname));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipes);
        }

        [HttpGet("filter/{categoryName}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        [ProducesResponseType(400)]
        public IActionResult GetFilter(string categoryName)
        {
            var recipes = _mapper.Map<List<RecipeList>>(_recipeRepository.GetFilter(categoryName));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipes);
        }

        [HttpGet("filter_search/{categoryName}/{searchname}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        [ProducesResponseType(400)]
        public IActionResult GetFilter_Search(string categoryName, string searchname)
        {
            var filter_recipes = _mapper.Map<List<RecipeList>>(_recipeRepository.GetFilter_Searcch(categoryName, searchname));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(filter_recipes);
        }
    }
}
