using BlazorApp5.Helpers;

namespace BlazorApp5.ServiceInterfaces
{
    public interface IHttpClientService : IDisposable
    {
        Task<ApiResponse> Get(string path, bool addAuthHeader);
        Task<ApiResponse> Get(string path, bool addAuthHeader, int id);

        Task<ApiResponse> Post(string path, bool addAuthHeader, object model);
        Task<ApiResponse> Put(string path, bool addAuthHeader, int id, object model);
        Task<ApiResponse> Delete(string path, bool addAuthHeader, int id);
    }
}
