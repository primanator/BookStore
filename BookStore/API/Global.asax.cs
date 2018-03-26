namespace API
{
    using AutoMapper;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles("API");
                cfg.AddProfiles("BLL");
            });
        }
    }
}
