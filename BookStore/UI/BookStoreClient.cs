using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace UI
{
    class BookStoreClient
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter Action:");
                string id = Console.ReadLine();

                GetRequest(id).Wait();
                Console.ReadKey();
                Console.Clear();
            }
        }

        static async Task GetRequest(string ID)
        {
            switch (ID)
            {
                case "Get":
                    Console.WriteLine("Enter id:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50402/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response;

                        //id == 0 means select all records    
                        if (id == 0)
                        {
                            response = await client.GetAsync("api/books");
                            if (response.IsSuccessStatusCode)
                            {
                                Book[] reports = await response.Content.ReadAsAsync<Book[]>();
                                foreach (var report in reports)
                                {
                                    Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}", report.Id, report.Name,
                                        report.Isbn, report.Pages, report.LimitedEdition);
                                }
                            }
                        }
                        else
                        {
                            response = await client.GetAsync("api/books/" + id);
                            if (response.IsSuccessStatusCode)
                            {
                                Book report = await response.Content.ReadAsAsync<Book>();
                                Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}", report.Id, report.Name,
                                    report.Isbn, report.Pages, report.LimitedEdition);
                            }
                        }
                    }
                    break;

                case "Post":
                    /*BookStoreClient newReport = new BookStoreClient();
                    Console.WriteLine("Enter data:");
                    Console.WriteLine("Enter Id:");
                    newReport.Id = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Name:");
                    newReport.Name = Console.ReadLine();
                    Console.WriteLine("Enter Isbn:");
                    newReport.Isbn = Console.ReadLine();
                    Console.WriteLine("Enter Pages:");
                    newReport.Pages = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Enter LimitedEdition:");
                    newReport.LimitedEdition = Boolean.Parse(Console.ReadLine());*/

                    Book newReport = new Book()
                    {
                        Id = 3,
                        Name = "Miguel de Cervantes",
                        Isbn = "9785699687275",
                        LimitedEdition = false,
                        Pages = 154,
                        WrittenIn = new DateTime(1605, 1, 1)
                    };

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50402/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.PostAsJsonAsync("api/books", newReport);

                        if (response.IsSuccessStatusCode)
                        {
                            bool result = await response.Content.ReadAsAsync<bool>();
                            if (result)
                                Console.WriteLine("Book created");
                            else
                                Console.WriteLine(response.Content);
                        }
                    }
                    break;

                case "Put":
                    Console.WriteLine("Enter id:");
                    Book update = new Book()
                    {
                        Id = 1,
                        Name = "The Young Man and The Land.",
                        Isbn = "6666666666",
                        Pages = 666,
                        LimitedEdition = true,
                        WrittenIn = new DateTime(1666, 1, 1)
                    };

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50402/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.PutAsJsonAsync("api/books", update);

                        if (response.IsSuccessStatusCode)
                        {
                            bool result = await response.Content.ReadAsAsync<bool>();
                            if (result)
                                Console.WriteLine("Book updated");
                            else
                                Console.WriteLine(response.Content);
                        }
                    }
                    break;

                case "Delete":
                    Console.WriteLine("Enter id:");
                    int delete = Convert.ToInt32(Console.ReadLine());
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50402/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.DeleteAsync("api/books/" + delete);

                        if (response.IsSuccessStatusCode)
                        {
                            bool result = await response.Content.ReadAsAsync<bool>();
                            if (result)
                                Console.WriteLine("Book deleted");
                            else
                                Console.WriteLine(response.Content);
                        }
                    }
                    break;
            }

        }

    }

    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public int Pages { get; set; }
        public bool LimitedEdition { get; set; }
        public DateTime? WrittenIn { get; set; }
    }
}