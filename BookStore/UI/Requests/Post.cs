namespace UI.Requests
{
    using System.Net;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Post : BaseRequest, IRequest
    {
        public Post(IContentSerializer contentSerializer, WebHeaderCollection headers, string requestUriString) : base(contentSerializer, headers, requestUriString)
        {
            _webRequest.Method = "POST";
            _webRequest.ContentType = "application/x-www-form-urlencoded";
        }
    }
}