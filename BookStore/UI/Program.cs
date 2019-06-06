namespace UI
{
    using System;
    using System.Net.Http.Headers;

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter command (get/post/put/delete/import):");
                new Client(
                    new Uri("http://localhost:50402/"),
                    new[] { new MediaTypeWithQualityHeaderValue("application/json") })
                    .SendRequest(Console.ReadLine());

                //using (BookStoreClient client = new BookStoreClient(new Uri("http://localhost:50402/"),
                //    new[] { new MediaTypeWithQualityHeaderValue("application/json") }))
                //{
                //    client.SendRequestAsync(Console.ReadLine()).Wait();
                //}

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}