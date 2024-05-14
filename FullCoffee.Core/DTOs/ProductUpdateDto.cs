namespace FullCoffee.Core.DTOs
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string? OldPrice { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
    }
}
