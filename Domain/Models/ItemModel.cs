namespace Domain.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
