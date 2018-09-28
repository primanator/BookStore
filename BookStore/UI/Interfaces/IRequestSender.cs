namespace UI.Interfaces
{
    using DTO.Entities;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    internal abstract class RequestSender<T>: IDisposable
        where T: Entity
    {
        protected HttpClient client;
        protected HttpResponseMessage response;

        public RequestSender()
        {
            //Uri baseAddress, IEnumerable< MediaTypeWithQualityHeaderValue > headers

            //_client = new HttpClient
            //{
            //    BaseAddress = baseAddress
            //};
            //_client.DefaultRequestHeaders.Accept.Clear();

            //foreach (var header in headers)
            //    _client.DefaultRequestHeaders.Accept.Add(header);
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task<T[]> SendAsync(){ return new T[] { }; }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        public void Dispose()
        {
            client.Dispose();
        }
    }
}