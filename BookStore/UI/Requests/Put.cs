namespace UI.Requests
{
    using System.Net;
    using UI.ContentProviders.Interfaces;
    using UI.Requests.Infrastructure;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal class Put<T> : BaseRequest<T>, IRequest
    {
        public Put(IGenericContentSerializer<T> contentSerializer, IContentProvider<T> contentProvider)
            : base(contentSerializer)
        {
            WebRequest = WebRequest.Create($"{BaseUri}/{contentProvider.GetName()}");
            WebRequest.Headers = contentProvider.GetHeaders();
            WebRequest.Method = "PUT";

            var requestObj = contentProvider.GetContent();
            WriteBytesToRequest(contentSerializer.ToBytes(requestObj));
        }
    }
}