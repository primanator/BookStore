namespace UI.Factories.ContentProviders
{
    using DTO.Entities;
    using UI.ContentProviders;
    using UI.ContentProviders.Interfaces;

    internal class ContentProviderFactory : IContentProviderFactory
    {
        public IContentProvider<T> GetDtoContentProvider<T>()
            where T: Dto, new()
        {
            return new DtoContentProvider<T>();
        }

        public IContentProvider<T> GetFileContentProvider<T>()
        {
            return new FileContentProvider<T>();
        }
    }
}