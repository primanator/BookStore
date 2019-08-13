namespace UI.Factory.Requests
{
    using System;
    using System.Configuration;
    using UI.Factory.Serializers;
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

        public IRequest DeleteRequest()
        {
            throw new System.NotImplementedException();
        }

        public IRequest GetAllRequest()
        {
            throw new System.NotImplementedException();
        }

        public IRequest GetRequest()
        {
            throw new System.NotImplementedException();
        }

        public IRequest PostMultipleRequest()
        {
            throw new System.NotImplementedException();
        }

        public IRequest PostRequest()
        {
            throw new System.NotImplementedException();
        }

        public IRequest PutRequest()
        {
            throw new System.NotImplementedException();
        }
    }
}
