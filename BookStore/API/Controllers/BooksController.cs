namespace API.Controllers
{
    using System.Web.Http;
    using DTO.Entities;
    using BLL.Interfaces;

    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService service)
        {
            _bookService = service;
        }

        public IHttpActionResult PostCreateBook(BookDto newBook)
        {
            if (!ModelState.IsValid || newBook == null)
                return BadRequest("Invalid model state");

            _bookService.Create(newBook);

            return Ok();
        }

        public IHttpActionResult GetAllBooks()
        {
            var books = _bookService.GetAll();

            return Ok(books);
        }

        public IHttpActionResult GetBookByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Parameter's value is empty.");

            var book = _bookService.GetSingle(name);

            return Ok(book);
        }

        public IHttpActionResult PutUpdateBook(BookDto freshBook)
        {
            if (!ModelState.IsValid || freshBook == null)
                return BadRequest("Invalid model state");

            _bookService.Update(freshBook);

            return Ok();
        }

        public IHttpActionResult DeleteBookByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Parameter's value is empty.");

            _bookService.Delete(name);

            return Ok();
        }
    }
}