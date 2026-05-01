namespace SchoolDMS.API.Models.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public IDictionary<string, string[]>? Errors { get; set; }

        public ApiResponse(bool success, string message, T? data, int statusCode, IDictionary<string, string[]>? errors = null)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = statusCode;
            Errors = errors;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation successful", int statusCode = 200)
        {
            return new ApiResponse<T>(true, message, data, statusCode);
        }

        public static ApiResponse<T> FailureResponse(string message, int statusCode = 400, IDictionary<string, string[]>? errors = null)
        {
            return new ApiResponse<T>(false, message, default, statusCode, errors);
        }
    }
}
