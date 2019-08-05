namespace UI.Requests
{
    using System.Net;
    using UI.Interfaces;

    internal class PostRequest : BaseRequest
    {
        public PostRequest(IPackageSerializer packageSerializer, WebHeaderCollection headers, string requestUriString) : base(packageSerializer, headers, requestUriString)
        {
            _webRequest.Method = "POST";
            _webRequest.ContentType = "application/x-www-form-urlencoded";
        }
    }
}