namespace API.Utils
{
    using BLL.Interfaces;
    using BLL.Services;
    using Ninject.Modules;

    public class ServiceModuleAPI : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookStoreService>().To<BookStoreService>();
        }
    }
}