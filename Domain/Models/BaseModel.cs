namespace Domain.Models
{
    public class BaseModel
    {
        public Guid GId { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
        public int Status { get; set; }
    }
}
