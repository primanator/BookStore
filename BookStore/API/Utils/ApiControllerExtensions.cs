namespace API.Utils
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public static class ApiControllerExtenstions
    {
        public static HttpResponseMessage ControllerErrorHttpResponse(this ApiController controller, string message)
        {
            return new HttpResponseMessage
            {
                Content = new StringContent(message),
                ReasonPhrase = "Server exception.",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }
}