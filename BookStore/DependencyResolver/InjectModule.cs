namespace DependencyResolver
{
    using System.Data.Entity;
    using Ninject.Modules;
    using BLL.Interfaces;
    using BLL.Services;
    using DAL.EF;
    using DAL.Implementation;
    using DAL.Interfaces;
    using DTO.Entities;
    using BLL.Utils;

    public class InjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<BookStoreContext>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IBookService>().To<BookService>();
            Bind<IImportService>().To<ImportService>();
            Bind<IValidator<BookDto>, ExcelFileValidator<BookDto>>();
        }
    }
}