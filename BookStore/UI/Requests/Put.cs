namespace UI.Requests
{
    using System.Net;
    using UI.ContentExtractors.Interfaces;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Put<T> : BaseRequest<T>, IRequest
    {
        public Put(IGenericContentSerializer<T> contentSerializer, IContentExtractor<T> contentExtractor, WebHeaderCollection headers, string requestUriString)
            : base(contentSerializer, headers, requestUriString)
        {
            _webRequest.Method = "PUT";
            RequestObj = contentExtractor.GetFullContent();
            WriteBytesToRequest(_contentSerializer.ToBytes(RequestObj));
        }
    }
}