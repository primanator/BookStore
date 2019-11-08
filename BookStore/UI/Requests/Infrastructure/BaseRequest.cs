namespace UI.Requests.Infrastructure
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using UI.Requests.Interfaces;
    using UI.Serializers.Interfaces;

    internal abstract class BaseRequest<T> : IRequest, IDisposable
    {
        private readonly IGenericContentSerializer<T> _contentSerializer;
        protected readonly string BaseUri;
        protected WebRequest WebRequest;
        protected WebResponse WebResponse;

        protected BaseRequest(IGenericContentSerializer<T> contentSerializer)
        {
            _contentSerializer = contentSerializer;

            BaseUri = ConfigurationManager.AppSettings["Uri"];
            if (string.IsNullOrEmpty(BaseUri))
            {
                throw new ArgumentNullException($"Empty {BaseUri} was passed to the {nameof(BaseRequest<T>)}");
            }
        }

        public void Send()
        {
            try
            {
                using (var responseStream = new StreamReader(WebRequest.GetResponse().GetResponseStream()))
                {
                    var response = responseStream.ReadToEnd();
                    Console.WriteLine(response);
                }
                //var response = ReadBytesFromResponse();
                //var response = _contentSerializer.FromBytes(responseBytes);
            }
            catch (WebException ex)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error occured while performing {nameof(WebRequest.Method)} request.\n{ex.Status}\n{ex.Response}\n{ex.InnerException}\n{ex.StackTrace}\n");
                Console.ResetColor();
            }
        }

        protected void WriteBytesToRequest(byte[] importBytes)
        {
            WebRequest.ContentLength = importBytes.Length;
            var requestStream = WebRequest.GetRequestStream();
            requestStream.Write(importBytes, 0, importBytes.Length);
            requestStream.Close();
        }

        //private byte[] ReadBytesFromResponse()
        //{
        //    using (var responseStream = WebResponse.GetResponseStream())
        //    {
        //        var html = new StreamReader(resp.GetResponseStream()).ReadToEnd();
        //        return binaryReader.ReadBytes((int)WebResponse.ContentLength);
        //    }
        //}

        public void Dispose()
        {
            WebResponse.Close();
        }
    }
}