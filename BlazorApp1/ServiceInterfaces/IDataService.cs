﻿namespace BlazorApp1.ServiceInterfaces
{
    public interface IDataService
    {
        public IItemService StockItems { get; }
    }
}
