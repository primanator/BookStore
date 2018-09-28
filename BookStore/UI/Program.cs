namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.ProcessInput().Wait();
        }
    }
}



//                foreach (var book in result)
//            Console.WriteLine("\nId:{0}\tName:{1}\tIsbn:{2}\tPages:{3}\tLimitedEdition:{4}", book.Id, book.Name,
//                book.Isbn, book.Pages, book.LimitedEdition);
//    else
//        Console.WriteLine("There are no books in the library.");
//}
//else
//{
//    Book book = await GetBookByTitleAsync(name);
//    if (book != null)
//        Console.WriteLine("\nId:{0}\tName:{1}\tIsbn:{2}\tPages:{3}\tLimitedEdition:{4}", book.Id, book.Name,
//            book.Isbn, book.Pages, book.LimitedEdition);
//    else
//        Console.WriteLine("There is no such book in the library.");