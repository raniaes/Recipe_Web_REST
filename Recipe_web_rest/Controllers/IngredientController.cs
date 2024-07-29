using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Recipe_web_rest.Dto;
using Recipe_web_rest.Interfaces;
using Recipe_web_rest.Models;
using Recipe_web_rest.Repository;

namespace Recipe_web_rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : Controller
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientController(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ingredient>))]
        public IActionResult GetIngredients()
        {
            var ingredients = _mapper.Map<List<IngredientDto>>(_ingredientRepository.GetIngredients());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ingredients);
        }

        [HttpGet("{ingredientId}")]
        [ProducesResponseType(200, Type = typeof(Ingredient))]
        [ProducesResponseType(400)]
        public IActionResult GetIngredient(int ingredientId)
        {
            if (!_ingredientRepository.IngredientExists(ingredientId))
            {
                return NotFound();
            }
            var ingredient = _mapper.Map<IngredientDto>(_ingredientRepository.GetIngredient(ingredientId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ingredient);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateIngredient([FromBody] IngredientDto ingredientCreate)
        {
            if (ingredientCreate == null)
                return BadRequest(ModelState);

            var ingredient = _ingredientRepository.GetIngredients()
                .Where(I => I.Name.Trim().ToUpper() == ingredientCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (ingredient != null)
            {
                ModelState.AddModelError("", "Ingredient already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredientMap = _mapper.Map<Ingredient>(ingredientCreate);

            if (!_ingredientRepository.CreateIngredient(ingredientMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{ingredientId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateIngredient(int ingredientId, [FromBody] IngredientDto updatedIngredient)
        {
            if (updatedIngredient == null)
                return BadRequest(ModelState);

            if (ingredientId != updatedIngredient.Id)
                return BadRequest(ModelState);

            if (!_ingredientRepository.IngredientExists(ingredientId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var ingredientMap = _mapper.Map<Ingredient>(updatedIngredient);

            if (!_ingredientRepository.UpdateIngredient(ingredientMap))
            {
                ModelState.AddModelError("", "Somthing went worng updating ingredient");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{ingredientId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteIngredient(int ingredientId)
        {
            if (!_ingredientRepository.IngredientExists(ingredientId))
                return NotFound();

            var ingredientToDelete = _ingredientRepository.GetIngredient(ingredientId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_ingredientRepository.DeleteIngredient(ingredientToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ingredient");
            }

            return NoContent();
        }
    }
}
