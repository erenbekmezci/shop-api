using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace Services.ExceptionHandlers
{
    public class CriticalExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
           
        {
            //business logic
            if (exception is CriticalException)
            {
                Console.WriteLine("hata ile iligli sms gönderildi");
            }

            return ValueTask.FromResult(false);
            /// Bu handler yalnızca loglama yapacak ve false dönerek diğer handler'lara izin verecek (GlobalExceptionHandler)

        }

       
    }
}
