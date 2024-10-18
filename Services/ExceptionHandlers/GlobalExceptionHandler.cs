using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            var errorsAsDto = ServiceResult.Fail(exception.Message,System.Net.HttpStatusCode.InternalServerError);

            httpContext.Response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            await httpContext.Response.WriteAsJsonAsync(errorsAsDto , cancellationToken);

            //İstemciye yanıt verildiğinden, başka bir middleware veya handler bu noktadan sonra çalışmaz. true ile yanıt döner
            return true;


        }
    }
}
