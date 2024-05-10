using System.Net;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using TasksManager.Domain.DTO;
using TasksManager.Domain.Exceptions;
using TasksManager.Domain.Variables;

namespace TasksManager.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        
        public ExceptionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                if (httpContext.Response.HasStarted) return;
                if (ValidateHandleSecretKeyAsync(httpContext).Result)
                {
                    await _next(httpContext);
                }

            }
            catch (ClientException ex)
            {
                await HandleClientExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleGlobalExceptionAsync(httpContext, ex);
            }
        }

        private async Task<bool> ValidateHandleSecretKeyAsync(HttpContext context)
        {
            bool result = true;
            if (context.Request.Path.Value == "/")
            {
                return result;
            }

            string? secretKeyAPI = _configuration[AppSettingsKeys.API_KEY];

            if (!context.Request.Headers.ContainsKey(AppSettingsKeys.API_KEY_ID))
            {
                result = false;
            }
            else if (string.IsNullOrEmpty(secretKeyAPI) || string.IsNullOrEmpty(context.Request.Headers[AppSettingsKeys.API_KEY_ID].ToString()))
            {
                result = false;

            }
            else if (!secretKeyAPI.Trim().ToUpper().Equals(context.Request.Headers[AppSettingsKeys.API_KEY_ID].ToString().Trim().ToUpper()))
            {
                result = false;
            }



            if (!result)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync(new ApiResponse<object?>(
                    statusCode: context.Response.StatusCode,
                    message: ServiceMessages.FORBIDDEN_MESSAGE,
                    response: null, 
                    ServiceMessages.FORBIDDEN
                ).ToString());
            }

            return result;

        }

        private async Task HandleGlobalExceptionAsync(HttpContext context, Exception ex)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ApiResponse<object?>(
                    statusCode: context.Response.StatusCode,
                    message: ServiceMessages.ERROR,
                    response: null, 
                    errorDescription: ex.StackTrace
                ).ToString());
        }

        private async Task HandleClientExceptionAsync(HttpContext context, ClientException ex)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(new ApiResponse<object?>(
                    statusCode: context.Response.StatusCode,
                    message: ex.Message,
                    response: null, 
                    errorDescription: ex.Message
                ).ToString());
        }
    }
}