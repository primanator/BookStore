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
    using DTO.QueryBuilders;
    using DTO.Entities;
    using BLL.Factory.Implementation.Excel;

    public class InjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<BookStoreContext>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<DtoFilterBuilder<BookDto>>().To<BookDtoFilterBuilder>();
            Bind<IBookService>().To<BookService>();
            Bind<IImportServiceFactory>().To<ExcelImportServiceFactory>().Named("Excel");
        }
    }
}