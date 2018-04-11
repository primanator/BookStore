namespace UI
{
    using System;
    using System.Net.Http.Headers;

    class Client
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter Http method:");

                using (BookStoreClient client = new BookStoreClient(new Uri("http://localhost:50402/"),
                    new[] { new MediaTypeWithQualityHeaderValue("application/json") }))
                {
                    client.SendRequestAsync(Console.ReadLine()).Wait();
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}