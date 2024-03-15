using BlazorApp1.Helpers;
using BlazorApp1.ServiceInterfaces.Internal;

namespace BlazorApp1.Services.Internal
{
    public class GenericService<TDto> : IGenericService<TDto> where TDto : class
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _controllerName;

        public GenericService(IHttpClientService httpClientService, string controllerName)
        {
            _httpClientService = httpClientService;
            _controllerName = controllerName;
        }


        public async Task<ApiResponse> Get()
        {

            return await _httpClientService.Get($"{_controllerName}/get", false);
        }

        public async Task<ApiResponse> Get(int id)
        {
            return await _httpClientService.Get($"{_controllerName}/get", false, id);
        }

        public async Task<ApiResponse> Create(TDto modelDto)
        {
            return await _httpClientService.Post($"{_controllerName}/create", false, modelDto);
        }

        public async Task<ApiResponse> Update(int id, TDto modelDto)
        {
            return await _httpClientService.Put($"{_controllerName}/update", false, id, modelDto);
        }

        public async Task<ApiResponse> Delete(int id)
        {
            return await _httpClientService.Delete($"{_controllerName}/delete", false, id);
        }
    }
}
