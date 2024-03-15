using Microsoft.AspNetCore.Http;
using System.Text;

namespace Application.Helpers
{
    public static class MessageBuilder
    {
        public static string BuildExceptionMessage(HttpContext context, Exception exception)
        {
            var msgBuilder = new StringBuilder();

            msgBuilder.AppendLine($"<h1>Message: {exception.Message}</h1>");

            return msgBuilder.ToString();

        }
    }
}
