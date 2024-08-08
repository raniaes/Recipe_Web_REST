using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
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
        public static IWebHostEnvironment _environment;

        public RecipeController(IRecipeRepository recipeRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, IMapper mapper, IWebHostEnvironment environment)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _environment = environment;
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
        [ProducesResponseType(200, Type = typeof(RecipeImageDto))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipe(int RecipeId)
        {
            if (!_recipeRepository.RecipeExists(RecipeId))
            {
                return NotFound();
            }

            var recipe = _mapper.Map<RecipeImageDto>(_recipeRepository.GetRecipe(RecipeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fileName = Path.GetFileName(recipe.Pic_address);
            var imagePath = Path.Combine(_environment.WebRootPath, "upload", fileName);
          
            if (System.IO.File.Exists(imagePath))
            {
                //recipe.ImageData = System.IO.File.ReadAllBytes(imagePath);

                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                recipe.ImageData = Convert.ToBase64String(imageBytes);
            }
            else
            {
                recipe.ImageData = null;
            }

            return Ok(recipe);
        }

        [HttpGet("images/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var filePath = Path.Combine(_environment.WebRootPath, "upload", fileName);

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
        public async Task<IActionResult> CreateRecipe(
            [FromQuery] int userId, 
            [FromQuery] int categoryId, 
            [FromQuery] int[] ingreId, 
            [FromForm] IFormFile file, 
            [FromForm] string name, 
            [FromForm] string instruction)
        {
            try
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(instruction))
                {
                    ModelState.AddModelError("", "Name and instruction are required.");
                    return BadRequest(ModelState);
                }

                var recipe = _recipeRepository.GetRecipes()
                .Where(c => c.Name.Trim().ToUpper() == name.Trim().ToUpper())
                .FirstOrDefault();

                if (recipe != null)
                {
                    ModelState.AddModelError("", "Recipe already exists");
                    return StatusCode(422, ModelState);
                }

                var recipeMap = new Recipe
                {
                    Name = name,
                    Instruction = instruction,
                    UserId = userId,
                    CategoryId = categoryId
                };

                if (file != null && file.Length > 0)
                {
                    var uploadDir = Path.Combine(_environment.WebRootPath, "upload");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    var fileName = $"{recipeMap.Name}_{DateTime.Now:ddMMyyyy}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    recipeMap.Pic_address = filePath;
                }

                if (!_recipeRepository.CreateRecipe(ingreId, recipeMap))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

                return Ok("Successfully created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }   

        [HttpPut("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRecipe(
            int recipeId,
            [FromQuery] int userId,
            [FromQuery] int categoryId,
            [FromQuery] int[] ingreId,
            [FromForm] IFormFile? file,
            [FromForm] int id,
            [FromForm] string name,
            [FromForm] string instruction)
            
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(instruction))
            {
                ModelState.AddModelError("", "Name and instruction are required.");
                return BadRequest(ModelState);
            }

            if (recipeId != id)
            {
                ModelState.AddModelError("", "Recipe ID mismatch.");
                return BadRequest(ModelState);
            }

            var existingRecipe = _recipeRepository.GetRecipe(recipeId);
            if (existingRecipe == null)
            {
                return NotFound();
            }

            existingRecipe.Name = name; 
            existingRecipe.Instruction = instruction; 
            existingRecipe.UserId = userId; 
            existingRecipe.CategoryId = categoryId; 

            if (file != null && file.Length > 0)
            {
                if (!string.IsNullOrEmpty(existingRecipe.Pic_address))
                {
                    if (System.IO.File.Exists(existingRecipe.Pic_address))
                    {
                        System.IO.File.Delete(existingRecipe.Pic_address);
                    }
                }

                var uploadDir = Path.Combine(_environment.WebRootPath, "upload");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                var fileName = $"{existingRecipe.Name}_{DateTime.Now:ddMMyyyy}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                existingRecipe.Pic_address = filePath;
            }

            

            if (!_recipeRepository.UpdateRecipe(ingreId, existingRecipe))
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

        public IActionResult DeleteRecipe(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
            {
                return NotFound();
            }

            var recipeToDelete = _recipeRepository.GetRecipe(recipeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pic_name = "";
            int lastindex = recipeToDelete.Pic_address.LastIndexOf('\\');
            if (lastindex >= 0)
            {
                pic_name = recipeToDelete.Pic_address.Substring(lastindex + 1);
            }

            if (!string.IsNullOrEmpty(pic_name))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload", pic_name);
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
