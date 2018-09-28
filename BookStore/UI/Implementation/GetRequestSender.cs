namespace UI.Implementation
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using DTO.Entities;
    using Interfaces;

    internal class GetRequestSender<T> : RequestSender<T>, IDisposable
        where T : Entity
    {
        private T[] _result;

        public override async Task<T[]> SendAsync()
        {
            Console.WriteLine("Enter item name (leave empty to choose all):");
            string name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                await GetAllAsync();
            }
            else
            {
                await GetSingleByNameAsync(name);
            }

            return _result.ToArray();
        }

        private async Task GetAllAsync()
        {
            try
            {
                response = await client.GetAsync("api/books");
                if (response.IsSuccessStatusCode)
                {
                    _result = await response.Content.ReadAsAsync<T[]>();
                }
                else
                {
                    Console.WriteLine("Request returned with failed status code: \n{0}\n{1}", response.StatusCode, response.ReasonPhrase);
                    
                }
            }
            catch (Exception e)
            {
                Console.Write("Error during request:\n{0}", e.Message);
            }
        }

        private async Task GetSingleByNameAsync(string name)
        {
            try
            {
                response = await client.GetAsync("api/books?name=" + name);
                if (response.IsSuccessStatusCode)
                {
                    _result = new T[] { await response.Content.ReadAsAsync<T>() };
                }
                else
                {
                    Console.WriteLine("Request returned with failed status code: \n{0}\n{1}", response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception e)
            {
                Console.Write("Error during request:\n{0}", e.Message);
            }
        }
    }
}
