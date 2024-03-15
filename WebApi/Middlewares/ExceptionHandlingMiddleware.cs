using Application.Helpers;
using System.Net;

namespace WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var source = !string.IsNullOrEmpty(ex.Source) ? ex.Source : string.Empty;

                if (env.IsDevelopment())
                {
                    var innerException = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                    var stackTrace = !string.IsNullOrEmpty(ex.StackTrace) ? ex.StackTrace.Replace("\r\n", Environment.NewLine).Trim() : string.Empty;
                    var htmlBody = MessageBuilder.BuildExceptionMessage(context, ex);
                    var response = ApiResponseBuilder.GenerateInternalServerError(null, $"{source}-{ex.Message}", stackTrace);
                    await context.Response.WriteAsJsonAsync(response);

                }
                else
                {
                    var response = ApiResponseBuilder.GenerateInternalServerError(null, $"{source}-{ex.Message}", null);
                    await context.Response.WriteAsJsonAsync(response);
                }
            }

        }
    }
}
