namespace UI
{
    using System;

    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public int Pages { get; set; }
        public bool LimitedEdition { get; set; }
        public DateTime WrittenIn { get; set; }
        public int LibraryId { get; set; }

        public static Book GetBookFromUserInput()
        {
            Book newBook = new Book();

            var properties = newBook.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.Name != "Id" && prop.Name != "LibraryId")
                {
                    Console.WriteLine("Please enter book's {0}", prop.Name);
                    var userValue = Console.ReadLine();
                    if (!string.IsNullOrEmpty(userValue))
                        prop.SetValue(newBook, Convert.ChangeType(userValue, prop.PropertyType));
                }
            }

            newBook.LibraryId = 1;
            return newBook;
        }

        public static Book CopyBookWithUserUpdates(Book bookToCopy)
        {
            var properties = bookToCopy.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.Name != "Id" && prop.Name != "Name" && prop.Name != "LibraryId")
                {
                    Console.WriteLine("Please enter book's {0}", prop.Name);
                    var userValue = Console.ReadLine();
                    if (!string.IsNullOrEmpty(userValue))
                        prop.SetValue(bookToCopy, Convert.ChangeType(userValue, prop.PropertyType));
                }
            }

            return bookToCopy;
        }
    }
}