namespace BLL.Utils
{
    using DAL.Implementation.UnitOfWork;
    using DAL.Interfaces.UnitOfWork;
    using Ninject.Modules;

    public class ServiceModuleBLL : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}