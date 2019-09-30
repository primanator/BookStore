namespace UI.Factories.ContentProviders
{
    using Contracts.Models;
    using UI.ContentProviders;
    using UI.ContentProviders.Interfaces;

    internal class ContentProviderFactory : IContentProviderFactory
    {
        public IContentProvider<T> GetContractContentProvider<T>()
            where T: BaseContract, new()
        {
            return new DtoContentProvider<T>();
        }

        public IContentProvider<T> GetFileContentProvider<T>()
        {
            return new FileContentProvider<T>();
        }
    }
}