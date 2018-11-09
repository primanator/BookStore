namespace API
{
    using AutoMapper;
    using ExceptionFilters;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new NotImplementedExceptionFilterAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new NullReferenceExceptionFilterAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new InvalidOperationExceptionFilterAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new HttpExceptionFilterAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new ArgumentExceptionFilterAttribute());

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles("DependencyResolver");
            });
        }
    }
}
