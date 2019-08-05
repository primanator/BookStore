namespace UI.Requests
{
    using System.Net;
    using UI.Interfaces;

    internal class GetRequest : BaseRequest
    {
        public GetRequest(IPackageSerializer packageSerializer, WebHeaderCollection headers, string requestUriString) : base(packageSerializer, headers, requestUriString)
        {
            _webRequest.Method = "GET";
        }
    }
}