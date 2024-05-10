using Newtonsoft.Json;

namespace TasksManager.Domain.DTO
{
    public class ApiResponse<T>
    {
        public int statusCode { get; set; }
        public string? errorDescription { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public ApiResponse(int statusCode, string message, T response, string? errorDescription = null)
        {
            this.statusCode = statusCode;
            this.message = message;
            data = response;
            this.errorDescription = errorDescription;
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
