namespace DAL.Utils
{
    using DAL.EF;
    using Ninject.Modules;
    using System.Data.Entity;

    public class ServiceModuleDAL : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(BookStoreContext)).To(typeof(DbContext));
        }
    }
}
