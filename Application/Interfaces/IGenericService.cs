namespace Application.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<int> Delete(int id);
    }
}
