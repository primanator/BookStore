namespace DependencyResolver
{
    using System.Data.Entity;
    using Ninject.Modules;
    using DAL.EF;
    using DAL.Implementation;
    using DAL.Interfaces;
    using BLL.Services.Implementation;
    using BLL.Services.Interfaces;
    using BLL.Factory.Interfaces;
    using BLL.Factory.Implementation;

    public class InjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<BookStoreContext>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IBookService>().To<BookService>();
            Bind<IImportServiceFactory>().To<ExcelImportServiceFactory>().Named("Excel");
        }
    }
}