namespace UI.Factory.Requests
{
    using DTO.Entities;
    using System;
    using System.Configuration;
    using UI.Factory.Serializers;
    using UI.Requests;
    using UI.Requests.Interfaces;

    internal class WebRequestFactory : IRequestFactory
    {
        private readonly ISerializerFactory _serializerFactory;
        private readonly string _baseUri;

        public WebRequestFactory(ISerializerFactory serializerFactory)
        {
            _serializerFactory = serializerFactory ?? throw new ArgumentNullException($"Empty {nameof(serializerFactory)} was passed to the {nameof(IRequestFactory)}");
            _baseUri = ConfigurationManager.AppSettings["Uri"];
            if (string.IsNullOrEmpty(_baseUri))
            {
                throw new ArgumentNullException($"Empty {_baseUri} was passed to the {nameof(IRequestFactory)}");
            }
        }

        public IRequest DeleteRequest<T>() where T: Dto, new()
        {
            var dtoSerializer = _serializerFactory.GetEntitySerializer<T, T>();
            return new Delete<T, T>(dtoSerializer, null, "books");
        }

        public IRequest GetRequest<T>() where T : Dto, new()
        {
            var dtoSerializer = _serializerFactory.GetEntitySerializer<T, T>();
            return new Get<T, T>(dtoSerializer, null, "books");
        }

        public IRequest PostWithXlsx<T>() where T : Dto, new()
        {
            throw new NotImplementedException();
            //var xlsxSerializer = _serializerFactory.GetXlsxSerializer();
            //return new Post<T, T>(xlsxSerializer, null, "import");
        }

        public IRequest PostRequest<T>() where T : Dto, new()
        {
            var dtoSerializer = _serializerFactory.GetEntitySerializer<T, T>();
            return new Post<T, T>(dtoSerializer, null, "books");
        }

        public IRequest PutRequest<T>() where T : Dto, new()
        {
            var dtoSerializer = _serializerFactory.GetEntitySerializer<T, T>();
            return new Put<T, T>(dtoSerializer, null, "books");
        }
    }
}
