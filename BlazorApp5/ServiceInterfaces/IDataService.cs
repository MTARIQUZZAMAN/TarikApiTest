namespace BlazorApp5.ServiceInterfaces
{
    public interface IDataService
    {
        public IItemService StockItems { get; }
    }
}
