namespace UI.Requests
{
    using System.Net;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Get : BaseRequest, IRequest
    {
        public Get(IContentSerializer contentSerializer, WebHeaderCollection headers, string requestUriString) : base(contentSerializer, headers, requestUriString)
        {
            _webRequest.Method = "GET";
        }
    }
}