namespace API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using DTO.Entities;
    using BLL.Interfaces;

    public class BooksController : ApiController
    {
        private readonly IBookStoreService _service;

        public BooksController(IBookStoreService service)
        {
            _service = service;
        }

        public IHttpActionResult PostCreateBook(Book newBook)
        {
            if (!ModelState.IsValid || newBook == null)
                return BadRequest("Invalid model state");

            try
            {
                _service.CreateBook(newBook);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(InternalErrorMessage(e.Message));
            }

            return Ok();
        }

        public IHttpActionResult GetAllBooks()
        {
            IEnumerable<Book> books = null;

            try
            {
                books = _service.GetAllBooks();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(InternalErrorMessage(e.Message));
            }

            return Ok(books);
        }

        public IHttpActionResult GetBookByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Parameter's value is empty.");

            Book book = null;

            try
            {
                book = _service.GetSingleBook(name);
                DeleteBookByName(name);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(InternalErrorMessage(e.Message));
            }

            return Ok(book);
        }

        public IHttpActionResult PutUpdateBook(Book freshBook)
        {
            if (!ModelState.IsValid || freshBook == null)
                return BadRequest("Invalid model state");

            try
            {
                _service.UpdateBook(freshBook);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(InternalErrorMessage(e.Message));
            }

            return Ok();
        }

        public IHttpActionResult DeleteBookByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Parameter's value is empty.");

            try
            {
                _service.DeleteBook(name);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(InternalErrorMessage(e.Message));
            }

            return Ok();
        }

        private HttpResponseMessage InternalErrorMessage(string message)
        {
            return new HttpResponseMessage
            {
                Content = new StringContent(message),
                ReasonPhrase = "Server exception.",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }
}