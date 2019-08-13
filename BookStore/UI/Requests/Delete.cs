namespace UI.Requests
{
    using System.Net;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Delete : BaseRequest, IRequest
    {
        public Delete(IContentSerializer contentSerializer, WebHeaderCollection headers, string requestUriString) : base(contentSerializer, headers, requestUriString)
        {
            _webRequest.Method = "DELETE";
        }
    }
}