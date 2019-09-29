namespace UI.Requests
{
    using System.Net;
    using UI.ContentExtractors.Interfaces;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Delete<T> : BaseRequest<T>, IRequest
    {
        public Delete(IGenericContentSerializer<T> contentSerializer, IContentExtractor<T> contentExtractor, WebHeaderCollection headers, string requestUriString)
            : base(contentSerializer, headers, requestUriString)
        {
            _webRequest.Method = "DELETE";
            RequestObj = (T)(object)contentExtractor.GetContentName();
        }
    }
}