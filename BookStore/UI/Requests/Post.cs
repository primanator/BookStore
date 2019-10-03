namespace UI.Requests
{
    using System.Net;
    using UI.ContentProviders.Interfaces;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Post<T> : BaseRequest<T>, IRequest
    {
        public Post(IGenericContentSerializer<T> contentSerializer, IContentProvider<T> contentProvider)
            : base(contentSerializer)
        {
            WebRequest = WebRequest.Create($"{BaseUri}/{contentProvider.GetContentPluralName()}");
            WebRequest.Headers = contentProvider.GetHeaders();
            WebRequest.Method = "POST";
            WebRequest.ContentType = "application/x-www-form-urlencoded";

            var requestObj = contentProvider.GetContent();
            WriteBytesToRequest(contentSerializer.ToBytes(requestObj));
        }
    }
}