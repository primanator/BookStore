namespace UI.Factory.Requests
{
    using DTO.Entities;
    using System;
    using UI.Factories.ContentProviders;
    using UI.Factory.Serializers;
    using UI.Requests;
    using UI.Requests.Interfaces;

    internal class WebRequestFactory : IRequestFactory
    {
        private readonly ISerializerFactory _serializerFactory;
        private readonly IContentProviderFactory _contentExtractorFactory;

        public WebRequestFactory(ISerializerFactory serializerFactory, IContentProviderFactory contentExtractorFactory)
        {
            _serializerFactory = serializerFactory ?? throw new ArgumentNullException($"Empty {nameof(serializerFactory)} was passed to the {nameof(IRequestFactory)}");
            _contentExtractorFactory = contentExtractorFactory ?? throw new ArgumentNullException($"Empty {nameof(contentExtractorFactory)} was passed to the {nameof(IRequestFactory)}");
        }

        public IRequest DeleteRequest<T>() where T : Dto, new()
        {
            var dtoSerializer = _serializerFactory.GetEntitySerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetDtoContentProvider<T>();
            return new Delete<T>(dtoSerializer, contentExtractor);
        }

        public IRequest GetRequest<T>() where T : Dto, new()
        {
            var dtoSerializer = _serializerFactory.GetEntitySerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetDtoContentProvider<T>();
            return new Get<T>(dtoSerializer, contentExtractor);
        }

        public IRequest PostRequest<T>() where T : Dto, new()
        {
            var dtoSerializer = _serializerFactory.GetEntitySerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetDtoContentProvider<T>();
            return new Post<T>(dtoSerializer, contentExtractor);
        }

        public IRequest PutRequest<T>() where T : Dto, new()
        {
            var dtoSerializer = _serializerFactory.GetEntitySerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetDtoContentProvider<T>();
            return new Put<T>(dtoSerializer, contentExtractor);
        }

        public IRequest PostWithXlsx<T>()
        {
            var xlsxSerializer = _serializerFactory.GetXlsxSerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetFileContentProvider<T>();
            return new Post<T>(xlsxSerializer, contentExtractor);
        }
    }
}
