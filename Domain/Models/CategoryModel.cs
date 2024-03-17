namespace Domain.Models
{
    public class CategoryModel : BaseModel
    {
        public CategoryModel()
        {
            Items = new List<ItemModel>();
        }
        public int Id { get; set; }
        public string? ItemName { get; set; } = string.Empty;
        public ICollection<ItemModel> Items { get; set; }
    }
}


//https://www.youtube.com/watch?v=NNiupSwiVfg

//https://www.youtube.com/watch?v=aPx5vyk00D8