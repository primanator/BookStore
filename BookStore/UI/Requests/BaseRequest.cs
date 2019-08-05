namespace UI.Requests
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using UI.Interfaces;

    internal abstract class BaseRequest : IRequest, IDisposable
    {
        protected IPackageSerializer _packageSerializer;
        protected WebRequest _webRequest;
        protected WebResponse _webResponse;

        protected BaseRequest(IPackageSerializer packageSerializer, WebHeaderCollection headers, string requestUriString)
        {
            _packageSerializer = packageSerializer;
            _webRequest = WebRequest.Create(ConfigurationManager.AppSettings["Uri"] + requestUriString);
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

        public void Send()
        {
            WriteBytesToRequest(_packageSerializer.GetBytes());

            var httpWebResponse = (HttpWebResponse)_webRequest.GetResponse();
            if (httpWebResponse.StatusCode == HttpStatusCode.Created)
            {
                var data = ReadBytesFromResponse();
                _packageSerializer.SaveBytes(data);
            }
            if (httpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                var data = ReadBytesFromResponse();
                Console.WriteLine(data);
            }
        }

        public void Dispose()
        {
            _webResponse.Close();
        }
    }
}