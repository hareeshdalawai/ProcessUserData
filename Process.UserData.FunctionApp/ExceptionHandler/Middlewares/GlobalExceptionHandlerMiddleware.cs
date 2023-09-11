using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace Process.UserData.FunctionApp.ExceptionHandler.Middlewares
{
    /// <summary>
    /// Implements exception handler that handles all uncaught and application exceptions
    /// </summary>
    public class GlobalExceptionHandlerMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly ILogger _logger;

        public GlobalExceptionHandlerMiddleware(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var exceptionToLog = exception is AggregateException ? exception.InnerException : exception;

                //log detailed error esponse here
                _logger.LogError(exceptionToLog, "");               
            }
        }
    }
}
