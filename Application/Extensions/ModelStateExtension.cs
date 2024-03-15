using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetModelStateErrors(this ModelStateDictionary dictionary)
        {
            var errors = dictionary.SelectMany(x => x.Value?.Errors).Select(x => x.ErrorMessage).ToList();
            return errors;
        }
    }
}
