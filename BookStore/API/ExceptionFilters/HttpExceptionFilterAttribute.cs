namespace API.ExceptionFilters
{
    using System.Web;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    public class HttpExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is HttpException)
            {
                context.Response = new HttpResponseMessage
                {
                    Content = new StringContent(context.Exception.Message),
                    StatusCode = HttpStatusCode.InternalServerError,
                    ReasonPhrase = "Server exception."
                };
            }
        }
    }
}