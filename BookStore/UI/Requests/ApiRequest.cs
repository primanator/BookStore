namespace UI
{
    using System;
    using System.Collections.Specialized;
    using System.Net;

    internal abstract class ApiRequest : IDisposable
    {
        protected WebRequest _webRequest;
        protected WebResponse _webResponse;

        protected ApiRequest(string requestUriString, WebHeaderCollection headers)
        {
            _webRequest = WebRequest.Create(requestUriString);
            _webRequest.Headers = headers ?? new WebHeaderCollection();
        }

        public void Dispose()
        {
            _webResponse.Close();
        }
    }
}
