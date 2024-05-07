using Microsoft.AspNetCore.Mvc;
using System.Net;
using TasksManager.Domain.DTO;

namespace TasksManager.API.Controllers
{
    public class BaseController: ControllerBase
    {
        public async Task<ObjectResult> GetResponseAsync<T>(HttpStatusCode statusCode, string message, T response, string? errorDescription = null)
        {
            return await Task.Run(() =>
            {
                var objectResult = new ApiResponse<T>((int)statusCode, message, response, errorDescription);
                return new ObjectResult(objectResult)
                {
                    StatusCode = (int)statusCode
                };
            });
        }
    }
}
