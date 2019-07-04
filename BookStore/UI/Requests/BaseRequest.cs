namespace UI.Requests
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;

    internal abstract class BaseRequest : IDisposable
    {
        protected WebRequest _webRequest;
        protected WebResponse _webResponse;

        protected BaseRequest(WebHeaderCollection headers, string requestUriString)
        {
            var baseUriString = ConfigurationManager.AppSettings["Uri"];

            _webRequest = WebRequest.Create(baseUriString + requestUriString);
            _webRequest.Headers = headers ?? new WebHeaderCollection();
        }

        protected void WriteBytesToRequest(byte[] importBytes)
        {
            _webRequest.ContentLength = importBytes.Length;
            var requestStream = _webRequest.GetRequestStream();
            requestStream.Write(importBytes, 0, importBytes.Length);
            requestStream.Close();
        }

        protected byte[] ReadBytesFromResponse()
        {
            var responseLength = (int)_webResponse.ContentLength;
            var responseBytes = new byte[responseLength];

            using (var binaryReader = new BinaryReader(_webResponse.GetResponseStream()))
            {
                responseBytes = binaryReader.ReadBytes(responseLength);
            }
            return responseBytes;
        }

        public void Dispose()
        {
            _webResponse.Close();
        }
    }
}