namespace BLL.Utils
{
    using DAL.Implementation;
    using DAL.Interfaces;
    using Ninject.Modules;

    public class ServiceModuleBLL : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}