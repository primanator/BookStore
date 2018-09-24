namespace DAL.Utils
{
    using EF;
    using Ninject.Modules;
    using System.Data.Entity;

    public class ServiceModuleDAL : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<BookStoreContext>();
        }
    }
}
