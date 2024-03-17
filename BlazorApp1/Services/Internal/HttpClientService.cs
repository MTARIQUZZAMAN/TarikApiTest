using BlazorApp1.Helpers;
using BlazorApp1.ServiceInterfaces.Internal;
using System.Net.Http.Json;

namespace BlazorApp1.Services.Internal
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WebApiClient");
        }

        private Task<bool> AddAuthHeader()
        {
            throw new NotImplementedException();
        }


        #region httpClient verbs
        public async Task<ApiResponse> Get(string path, bool addAuthHeader)
        {

            if (addAuthHeader == false)
                return await GetResponse(path);


            bool authHeaderAdded = await AddAuthHeader();
            return addAuthHeader == true ? await GetResponse(path) : ApiResponseBuilder.GenerateUnauthorized("Unauthorised", "Login");


        }

        public async Task<ApiResponse> Get(string path, bool addAuthHeader, int id)
        {
            if (addAuthHeader == false)
                return await GetResponse(path, id);


            bool authHeaderAdded = await AddAuthHeader();
            return addAuthHeader == true ? await GetResponse(path, id) : ApiResponseBuilder.GenerateUnauthorized("Unauthorised", "Login");

        }

        public async Task<ApiResponse> Get(string path, bool addAuthHeader, int? id)
        {
            if (addAuthHeader == false)
                return await GetResponse(path, id);


            bool authHeaderAdded = await AddAuthHeader();
            return addAuthHeader == true ? await GetResponse(path, id) : ApiResponseBuilder.GenerateUnauthorized("Unauthorised", "Login");

        }

        public async Task<ApiResponse> Post(string path, bool addAuthHeader, object model)
        {
            try
            {
                if (addAuthHeader == false)
                    return await PostResponse(path, model);

                bool authHeaderAdded = await AddAuthHeader();
                return addAuthHeader == true ? await PostResponse(path, model) : ApiResponseBuilder.GenerateUnauthorized("Unauthorised", "Login");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw ex;
            }

        }

        public async Task<ApiResponse> Put(string path, bool addAuthHeader, int id, object model)
        {
            if (addAuthHeader == false)
                return await PutResponse(path, id, model);


            bool authHeaderAdded = await AddAuthHeader();
            return addAuthHeader == true ? await PutResponse(path, id, model) : ApiResponseBuilder.GenerateUnauthorized("Unauthorised", "Login");

        }

        public async Task<ApiResponse> Delete(string path, bool addAuthHeader, int id)
        {
            if (addAuthHeader == false)
                return await DeleteResponse(path, id);


            bool authHeaderAdded = await AddAuthHeader();
            return addAuthHeader == true ? await DeleteResponse(path, id) : ApiResponseBuilder.GenerateUnauthorized("Unauthorised", "Login");

        }

        #endregion



        #region response getters
        private async Task<ApiResponse> GetResponse(string path)
        {
            var httpResponseMessage = await _httpClient.GetAsync(path);
            return await httpResponseMessage.Content.ReadFromJsonAsync<ApiResponse>();
        }
        private async Task<ApiResponse> GetResponse(string path, int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{path}/{id}");
            return await httpResponseMessage.Content.ReadFromJsonAsync<ApiResponse>();
        }
        private async Task<ApiResponse> GetResponse(string path, int? id)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{path}/{id}");
            return await httpResponseMessage.Content.ReadFromJsonAsync<ApiResponse>();
        }

        private async Task<ApiResponse> PostResponse(string path, object model)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(path, model);
            return await httpResponseMessage.Content.ReadFromJsonAsync<ApiResponse>();
        }
        private async Task<ApiResponse> PutResponse(string path, int id, object model)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync($"{path}/{id}", model);
            return await httpResponseMessage.Content.ReadFromJsonAsync<ApiResponse>();
        }
        private async Task<ApiResponse> DeleteResponse(string path, int id)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync($"{path}/{id}");
            return await httpResponseMessage.Content.ReadFromJsonAsync<ApiResponse>();
        }
        #endregion


        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
