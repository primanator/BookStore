namespace UI.Requests.Infrastructure
{
    using System;
    using System.IO;
    using System.Net;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal abstract class BaseRequest<Ts, Td> : IRequest, IDisposable
    {
        protected IGenericContentSerializer<Ts, Td> _contentSerializer;
        protected WebRequest _webRequest;
        protected WebResponse _webResponse;

        protected BaseRequest(IGenericContentSerializer<Ts, Td> contentSerializer, WebHeaderCollection headers, string requestUriString)
        {
            _contentSerializer = contentSerializer;
            _webRequest = WebRequest.Create(requestUriString);
            _webRequest.Headers = headers ?? new WebHeaderCollection();
        }

        public void Send()
        {
            var requestObj = _contentSerializer.GetContent();
            var requestBytes = _contentSerializer.ToBytes(requestObj);
            WriteBytesToRequest(requestBytes);

            try
            {
                _webResponse = _webRequest.GetResponse();
                var responseBytes = ReadBytesFromResponse();
                var response = _contentSerializer.FromBytes(responseBytes);
                Console.WriteLine(response);
            }
            catch (WebException ex)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error occured while performing {nameof(_webRequest.Method)} request.\n{ex.Status}\n{ex.Response}\n{ex.InnerException}\n{ex.StackTrace}\n");
                Console.ResetColor();
            }
        }

        private void WriteBytesToRequest(byte[] importBytes)
        {
            _webRequest.ContentLength = importBytes.Length;
            var requestStream = _webRequest.GetRequestStream();
            requestStream.Write(importBytes, 0, importBytes.Length);
            requestStream.Close();
        }

        private byte[] ReadBytesFromResponse()
        {
            using (var binaryReader = new BinaryReader(_webResponse.GetResponseStream()))
            {
                return binaryReader.ReadBytes((int)_webResponse.ContentLength);
            }
        }

        public void Dispose()
        {
            _webResponse.Close();
        }
    }
}