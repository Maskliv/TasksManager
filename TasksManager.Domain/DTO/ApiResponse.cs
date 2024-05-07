using Newtonsoft.Json;

namespace TasksManager.Domain.DTO
{
    public class ApiResponse<T>
    {
        #region Properties
        public int StatusCode { get; set; }
        public string? ErrorDescription { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="response"></param>
        public ApiResponse(int statusCode, string message, T response, string? ErrorDescription = null)
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.Data = response;
            this.ErrorDescription = ErrorDescription;
        }

        #endregion Constructor

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
