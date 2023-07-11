using static DocumenWorker.DB.API.Data.Const;

namespace DocumenWorker.DB.API.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request
                .Headers
                .SingleOrDefault(header => header.Key == HeaderKeys.CORRELATION_ID)
                .Value;

            if (string.IsNullOrEmpty(correlationId))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Отсутствует заголовок \"correlationId\" в запросе");
                return;
            }

            if (!Guid.TryParse(correlationId, out Guid guidCorrelationId))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Заголовок \"correlationId\" имеет неправильный формат. Ожидаемый формат - Guid.");
                return;
            }

            await _next(context);
        }
    }
}
