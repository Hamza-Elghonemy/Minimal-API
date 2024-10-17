using System.Net;

namespace MagicVanilla_CouponAPI.Models
{
    public class API_Response
    {
        public API_Response()
        {
            ErrorMessage = new List<string>();
        }   
        public object Result { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
}
