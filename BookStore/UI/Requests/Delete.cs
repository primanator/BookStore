namespace UI.Requests
{
    using System.Net;
    using UI.ContentProviders.Interfaces;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Delete<T> : BaseRequest<T>, IRequest
    {
        public Delete(IGenericContentSerializer<T> contentSerializer, IContentProvider<T> contentProvider)
            : base(contentSerializer)
        {
            WebRequest = WebRequest.Create($"{BaseUri}/{contentProvider.GetName()}");
            WebRequest.Headers = contentProvider.GetHeaders();
            WebRequest.Method = "DELETE";
        }
    }
}