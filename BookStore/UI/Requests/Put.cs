namespace UI.Requests
{
    using System.Net;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Put<Ts, Td> : BaseRequest<Ts, Td>, IRequest
    {
        public Put(IGenericContentSerializer<Ts, Td> contentSerializer, WebHeaderCollection headers, string requestUriString)
            : base(contentSerializer, headers, requestUriString)
        {
            _webRequest.Method = "PUT";
        }
    }
}