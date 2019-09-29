namespace UI.Factories.ContentExtractors
{
    using DTO.Entities;
    using UI.ContentExtractors;
    using UI.ContentExtractors.Interfaces;

    internal class ContentExtractorFactory : IContentExtractorFactory
    {
        public IContentExtractor<T> GetDtoContentExtractor<T>()
            where T: Dto, new()
        {
            return new DtoContentExtractor<T>();
        }

        public IContentExtractor<T> GetFileContentExtractor<T>()
        {
            return new FileContentExtractor<T>();
        }
    }
}