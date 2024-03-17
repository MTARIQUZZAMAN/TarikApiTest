namespace Domain.Models
{
    public class ItemModel : BaseModel
    {
        public int Id { get; set; }
        public string? ItemName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public CategoryModel? Category { get; set; }

    }
}
