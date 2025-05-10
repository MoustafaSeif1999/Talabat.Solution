namespace Talabat.APIs.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string Details { get; set; }


        public ApiExceptionResponse( int status_Code , string Message = null , string details = null ) : base( status_Code , Message )
        {
            Details = details;
        }

    }
}
