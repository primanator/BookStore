namespace API.ExceptionFilters
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    public class ArgumentExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ArgumentException || context.Exception is ArgumentNullException || context.Exception is ArgumentOutOfRangeException)
            {
                context.Response = new HttpResponseMessage
                {
                    Content = new StringContent(context.Exception.Message),
                    StatusCode = HttpStatusCode.BadRequest,
                    ReasonPhrase = "Argument exception."
                };
            }
        }
    }
}