namespace API.Utils
{
    using BLL.Interfaces;
    using BLL.Services;
    using Ninject.Modules;

    public class ServiceModuleAPI : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookService>().To<BookService>();
            Bind<IDocumentService>().To<DocumentService>();
        }
    }
}