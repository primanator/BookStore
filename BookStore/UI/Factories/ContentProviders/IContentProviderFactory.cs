namespace UI.Factories.ContentProviders
{
    using DTO.Entities;
    using UI.ContentProviders.Interfaces;

    public interface IContentProviderFactory
    {
        IContentProvider<T> GetDtoContentProvider<T>() where T: Dto, new();

        IContentProvider<T> GetFileContentProvider<T>();
    }
}