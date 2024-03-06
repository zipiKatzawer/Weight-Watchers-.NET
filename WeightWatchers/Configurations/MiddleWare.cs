using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json.Serialization;
using System.Net;

namespace Project_Net.core.config
{
    public class MiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<MiddleWare> _logger;

        public MiddleWare(RequestDelegate next, ILogger<MiddleWare> logger)
        {
            this.next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"Request received at: {DateTime.Now}");
            try
            {
                await next(context);
                // Log success if no  occurred
                _logger.LogInformation("Middleware processing completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred during middleware processing.");

                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {



            var response = new { error = ex.Message };
            var jsonResponse = JsonConvert.SerializeObject(response);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            _logger.LogInformation("Exception handled successfully.");


            await context.Response.WriteAsync(jsonResponse);

        }

    }
}
