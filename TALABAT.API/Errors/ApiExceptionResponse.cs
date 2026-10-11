namespace TALABAT.API.Errors
{
    public class ApiExceptionResponse :ApiResponse
    {
        public string? details { get; set; }

        public ApiExceptionResponse(int StatusCode, string? Message = null, string? Details = null) : base(StatusCode, Message)
        {
            details = Details;
        }
    }
}
