namespace UI.Requests
{
    using System.Net;
    using UI.Serializers;

    internal class PostRequest : BaseRequest
    {
        private readonly IPackageSerializer _packageSerializer;

        public PostRequest(IPackageSerializer packageSerializer, WebHeaderCollection headers, string requestUriString) : base(headers, requestUriString)
        {
            _packageSerializer = packageSerializer;
            _webRequest.Method = "POST";
            _webRequest.ContentType = "application/x-www-form-urlencoded";
        }

        public HttpStatusCode Send()
        {
            WriteBytesToRequest(_packageSerializer.GetBytes());

            var httpWebResponse = (HttpWebResponse)_webRequest.GetResponse();
            if (httpWebResponse.StatusCode == HttpStatusCode.Created)
            {
                var data = ReadBytesFromResponse();
                _packageSerializer.SaveBytes(data);
            }

            return httpWebResponse.StatusCode;
        }
    }
}
