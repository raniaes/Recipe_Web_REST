namespace Recipe_web_rest.Request
{
    public class RecipeList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public string Pic_address { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
