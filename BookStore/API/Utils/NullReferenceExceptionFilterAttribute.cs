namespace API.Utils
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    public class NullReferenceExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NullReferenceException)
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