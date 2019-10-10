namespace UI.Factory.Requests
{
    using Contracts.Models;
    using System;
    using UI.Factories.ContentProviders;
    using UI.Factory.Serializers;
    using UI.Requests;
    using UI.Requests.Interfaces;

    internal class WebRequestFactory : IRequestFactory
    {
        private readonly ISerializerFactory _serializerFactory;
        private readonly IContentProviderFactory _contentExtractorFactory;

        public WebRequestFactory(ISerializerFactory serializerFactory, IContentProviderFactory contentProviderFactory)
        {
            _serializerFactory = serializerFactory ?? throw new ArgumentNullException($"Empty {nameof(serializerFactory)} was passed to the {nameof(IRequestFactory)}");
            _contentExtractorFactory = contentProviderFactory ?? throw new ArgumentNullException($"Empty {nameof(contentProviderFactory)} was passed to the {nameof(IRequestFactory)}");
        }

        public IRequest DeleteRequest<T>() where T : BaseContract, new()
        {
            var dtoSerializer = _serializerFactory.GetContractSerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetContractContentProvider<T>();
            return new Delete<T>(dtoSerializer, contentExtractor);
        }

        public IRequest GetRequest<T>() where T : BaseContract, new()
        {
            var dtoSerializer = _serializerFactory.GetContractSerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetContractContentProvider<T>();
            return new Get<T>(dtoSerializer, contentExtractor);
        }

        public IRequest PostRequest<T>() where T : BaseContract, new()
        {
            var dtoSerializer = _serializerFactory.GetContractSerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetContractContentProvider<T>();
            return new Post<T>(dtoSerializer, contentExtractor);
        }

        public IRequest PutRequest<T>() where T : BaseContract, new()
        {
            var dtoSerializer = _serializerFactory.GetContractSerializer<T>();
            var contentExtractor = _contentExtractorFactory.GetContractContentProvider<T>();
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
