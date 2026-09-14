namespace TALABAT.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int statusCode, string? message =null)
        {
            StatusCode = statusCode;
            Message = message ??GetDefaultMessage(statusCode);
        }
        private string? GetDefaultMessage(int? statusCode) => statusCode switch
        {
            200 => "Success",
            400 => "Bad request.",
            401 => "Unauthorized access.",
            404 => "Resource not found.",
            500 => "Internal server error.",
            _ => null
        };


    }
}
