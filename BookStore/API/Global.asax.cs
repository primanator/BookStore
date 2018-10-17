namespace API
{
    using AutoMapper;
    using Utils;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new NotImplementedExceptionFilterAttribute());

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles("DependencyResolver");
            });
        }
    }
}
