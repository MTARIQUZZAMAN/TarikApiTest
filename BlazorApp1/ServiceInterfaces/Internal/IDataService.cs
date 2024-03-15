namespace BlazorApp1.ServiceInterfaces.Internal
{
    public interface IDataService
    {
        public IItemService StockItems { get; }
        public ICategoryService CategoryItems { get; }
    }
}
