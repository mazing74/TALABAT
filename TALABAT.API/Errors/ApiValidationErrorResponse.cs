namespace TALABAT.API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {

        public IEnumerable<string> errors { get; set; }
        public ApiValidationErrorResponse() : base(400)
        {
            errors = new List<string>();
        }
    }
}
