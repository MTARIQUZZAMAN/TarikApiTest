using BlazorApp1.Helpers;

namespace BlazorApp1.ServiceInterfaces.Internal
{
    public interface IGenericService<TDto> where TDto : class
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int Id);
        Task<ApiResponse> Create(TDto modelDto);
        Task<ApiResponse> Update(int id, TDto modelDto);
        Task<ApiResponse> Delete(int id);
    }
}
