namespace UI.Requests
{
    using System.Net;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Put : BaseRequest, IRequest
    {
        public Put(IContentSerializer contentSerializer, WebHeaderCollection headers, string requestUriString) : base(contentSerializer, headers, requestUriString)
        {
            _webRequest.Method = "PUT";
        }
    }
}