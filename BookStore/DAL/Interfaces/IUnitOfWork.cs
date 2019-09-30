namespace DAL.Interfaces
{
    using Contracts.Models;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : BaseContract;

        void Save();
    }
}