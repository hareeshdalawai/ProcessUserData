using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Process.UserData.FunctionApp.ExceptionHandler.Middlewares;

namespace Process.UserData.FunctionApp.ExceptionHandler.Extensions
{

    /// <summary>
    /// Provides extension methods to register <c>GlobalExceptionHandlerMiddleware</c> with service provider.
    /// </summary>
    public static class FunctionWorkerExtensions
    {
        public static void UseGlobalExceptionHandler(this IFunctionsWorkerApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
