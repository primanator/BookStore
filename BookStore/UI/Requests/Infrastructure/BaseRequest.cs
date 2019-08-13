namespace UI.Requests.Infrastructure
{
    using System;
    using System.IO;
    using System.Net;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal abstract class BaseRequest : IRequest, IDisposable
    {
        protected IContentSerializer _contentSerializer;
        protected WebRequest _webRequest;
        protected WebResponse _webResponse;

        protected BaseRequest(IContentSerializer contentSerializer, WebHeaderCollection headers, string requestUriString)
        {
            _contentSerializer = contentSerializer;
            _webRequest = WebRequest.Create(requestUriString);
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
            using (var binaryReader = new BinaryReader(_webResponse.GetResponseStream()))
            {
                return binaryReader.ReadBytes((int)_webResponse.ContentLength);
            }
        }

        public void Send()
        {
            WriteBytesToRequest(_contentSerializer.ToBytes());

            try
            {
                _webResponse = _webRequest.GetResponse();
                var responseBytes = ReadBytesFromResponse();
                _contentSerializer.ReadBytes(responseBytes);
            }
            catch (WebException ex)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error occured while performing {nameof(_webRequest.Method)} request.\n{ex.Status}\n{ex.Response}\n{ex.InnerException}\n{ex.StackTrace}\n");
                Console.ResetColor();
            }
        }

        public void Dispose()
        {
            _webResponse.Close();
        }
    }
}