namespace UI.Requests
{
    using System.Net;
    using UI.ContentExtractors.Interfaces;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Get<T> : BaseRequest<T>, IRequest
    {
        public Get(IGenericContentSerializer<T> contentSerializer, IContentExtractor<T> contentExtractor, WebHeaderCollection headers, string requestUriString)
            : base(contentSerializer, headers, requestUriString)
        {
            _webRequest.Method = "GET";
            RequestObj = (T)(object)contentExtractor.GetContentName();
        }
    }
}