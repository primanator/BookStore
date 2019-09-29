namespace UI.Factories.ContentExtractors
{
    using DTO.Entities;
    using UI.ContentExtractors.Interfaces;

    public interface IContentExtractorFactory
    {
        IContentExtractor<T> GetDtoContentExtractor<T>() where T: Dto, new();

        IContentExtractor<T> GetFileContentExtractor<T>();
    }
}