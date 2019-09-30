namespace UI.Factories.ContentProviders
{
    using Contracts.Models;
    using UI.ContentProviders.Interfaces;

    public interface IContentProviderFactory
    {
        IContentProvider<T> GetContractContentProvider<T>() where T: BaseContract, new();

        IContentProvider<T> GetFileContentProvider<T>();
    }
}