namespace UI.Requests
{
    using System.Net;
    using UI.ContentProviders.Interfaces;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Get<T> : BaseRequest<T>, IRequest
    {
        public Get(IGenericContentSerializer<T> contentSerializer, IContentProvider<T> contentProvider)
            : base(contentSerializer)
        {
            WebRequest = WebRequest.Create($"{BaseUri}/{contentProvider.GetContentPluralName()}?name={contentProvider.GetRequiredItemName()}");
            WebRequest.Headers = contentProvider.GetHeaders();
            WebRequest.Method = "GET";
        }
    }
}