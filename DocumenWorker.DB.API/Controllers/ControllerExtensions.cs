using Microsoft.AspNetCore.Mvc;
using static DocumenWorker.DB.API.Data.Const;

namespace DocumenWorker.DB.API.Controllers
{
    public static class ControllerExtensions
    {
        public static Guid GetCorrelationId(this ControllerBase controller)
        {
            var correlationId = controller.HttpContext.Request
              .Headers
              .SingleOrDefault(header => header.Key == HeaderKeys.CORRELATION_ID)
              .Value;

            return string.IsNullOrEmpty(correlationId) ? Guid.Empty : Guid.Parse(correlationId);
        }
    }
}
