using System;

namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int Status_Code { get; set; }

        public string Message { get; set; }


        public ApiResponse( int status_code , string message = null )
        {
            Status_Code = status_code ;
            Message = message ?? GetMessageBasedOnStatusCode(status_code) ;
        }

        private static  string GetMessageBasedOnStatusCode(int status_code)
        {
            return status_code switch
            {
                400 => " A Bad request , You have made " ,
                401 => " Authorized , you are not " ,
                404 => " Resource found , it was not " ,
                500 => " Error at the Path " ,
                _ => null

            };
        }
    }
}
