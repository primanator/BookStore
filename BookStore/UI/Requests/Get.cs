namespace UI.Requests
{
    using System.Net;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Get<Ts, Td> : BaseRequest<Ts, Td>, IRequest
    {
        public Get(IGenericContentSerializer<Ts, Td> contentSerializer, WebHeaderCollection headers, string requestUriString)
            : base(contentSerializer, headers, requestUriString)
        {
            _webRequest.Method = "GET";
        }
    }
}