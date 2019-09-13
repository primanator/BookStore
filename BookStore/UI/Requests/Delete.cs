namespace UI.Requests
{
    using System.Net;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Delete<Ts, Td> : BaseRequest<Ts, Td>, IRequest
    {
        public Delete(IGenericContentSerializer<Ts, Td> contentSerializer, WebHeaderCollection headers, string requestUriString)
            : base(contentSerializer, headers, requestUriString)
        {
            _webRequest.Method = "DELETE";
        }
    }
}