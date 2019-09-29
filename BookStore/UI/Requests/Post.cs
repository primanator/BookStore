namespace UI.Requests
{
    using DTO.Entities;
    using System.Net;
    using UI.ContentExtractors.Interfaces;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Post<T> : BaseRequest<T>, IRequest
    {
        public Post(IGenericContentSerializer<T> contentSerializer, IContentExtractor<T> contentExtractor, WebHeaderCollection headers, string requestUriString)
            : base(contentSerializer, headers, requestUriString)
        {
            _webRequest.Method = "POST";
            _webRequest.ContentType = "application/x-www-form-urlencoded";

            if (typeof(T).IsSubclassOf(typeof(Dto)))
            {
                RequestObj = contentExtractor.GetFullContent();
                WriteBytesToRequest(_contentSerializer.ToBytes(RequestObj));
            }
            else
            {
                RequestObj = (T)(object)contentExtractor.GetContentName();
            }
        }
    }
}