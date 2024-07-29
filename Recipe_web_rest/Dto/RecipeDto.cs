using Recipe_web_rest.Models;

namespace Recipe_web_rest.Dto
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public string Pic_address { get; set; }
    }
}
