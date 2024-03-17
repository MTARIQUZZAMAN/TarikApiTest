using BlazorApp1.Helpers;

namespace BlazorApp1.ServiceInterfaces.Internal
{
    public interface IHttpClientService : IDisposable
    {
        Task<ApiResponse> Get(string path, bool addAuthHeader);
        Task<ApiResponse> Get(string path, bool addAuthHeader, int id);
        Task<ApiResponse> Get(string path, bool addAuthHeader, int? id);
        Task<ApiResponse> Post(string path, bool addAuthHeader, object model);
        Task<ApiResponse> Put(string path, bool addAuthHeader, int id, object model);
        Task<ApiResponse> Delete(string path, bool addAuthHeader, int id);
    }
}
