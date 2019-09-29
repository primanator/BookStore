namespace UI.Factory.Requests
{
    using DTO.Entities;
    using System;
    using System.Configuration;
    using UI.Factories.ContentExtractors;
    using UI.Factory.Serializers;
    using UI.Requests;
    using UI.Requests.Interfaces;

    internal class WebRequestFactory : IRequestFactory
    {
        private readonly ISerializerFactory _serializerFactory;
        private readonly IContentExtractorFactory _contentExtractorFactory;
        private readonly string _baseUri;

        public WebRequestFactory(ISerializerFactory serializerFactory, IContentExtractorFactory contentExtractorFactory)
        {
            _serializerFactory = serializerFactory ?? throw new ArgumentNullException($"Empty {nameof(serializerFactory)} was passed to the {nameof(IRequestFactory)}");
            _contentExtractorFactory = contentExtractorFactory ?? throw new ArgumentNullException($"Empty {nameof(contentExtractorFactory)} was passed to the {nameof(IRequestFactory)}");
            _baseUri = ConfigurationManager.AppSettings["Uri"];
            if (string.IsNullOrEmpty(_baseUri))
            {
                throw new ArgumentNullException($"Empty {_baseUri} was passed to the {nameof(IRequestFactory)}");
            }
        }

        public IRequest DeleteRequest<T>() where T : Dto, new()
        {
            var dtoSerializer = _serializerFactory.GetEntitySerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetDtoContentExtractor<T>();
            return new Delete<T>(dtoSerializer, contentExtractor, null, $"{_baseUri}/books");
        }

        public IRequest GetRequest<T>() where T : Dto, new()
        {
            var dtoSerializer = _serializerFactory.GetEntitySerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetDtoContentExtractor<T>();
            return new Get<T>(dtoSerializer, contentExtractor, null, $"{_baseUri}/books");
        }

        public IRequest PostRequest<T>() where T : Dto, new()
        {
            var dtoSerializer = _serializerFactory.GetEntitySerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetDtoContentExtractor<T>();
            return new Post<T>(dtoSerializer, contentExtractor, null, $"{_baseUri}/books");
        }

        public IRequest PutRequest<T>() where T : Dto, new()
        {
            var dtoSerializer = _serializerFactory.GetEntitySerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetDtoContentExtractor<T>();
            return new Put<T>(dtoSerializer, contentExtractor, null, $"{_baseUri}/books");
        }

        public IRequest PostWithXlsx<T>()
        {
            var xlsxSerializer = _serializerFactory.GetXlsxSerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetFileContentExtractor<T>();
            return new Post<T>(xlsxSerializer, contentExtractor, null, $"{_baseUri}/import");
        }
    }
}
