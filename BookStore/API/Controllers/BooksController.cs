namespace API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using DTO.Entities;
    using BLL.Interfaces;
    using API.Utils;

    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService service)
        {
            _bookService = service;
        }

        public IHttpActionResult PostCreateBook(Book newBook)
        {
            if (!ModelState.IsValid || newBook == null)
                return BadRequest("Invalid model state");

            try
            {
                _bookService.Create(newBook);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(this.ControllerErrorHttpResponse(e.Message));
            }

            return Ok();
        }

        public IHttpActionResult GetAllBooks()
        {
            IEnumerable<Book> books = null;

            try
            {
                books = _bookService.GetAll();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(this.ControllerErrorHttpResponse(e.Message));
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
                book = _bookService.GetSingle(name);
                DeleteBookByName(name);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(this.ControllerErrorHttpResponse(e.Message));
            }

            return Ok(book);
        }

        public IHttpActionResult PutUpdateBook(Book freshBook)
        {
            if (!ModelState.IsValid || freshBook == null)
                return BadRequest("Invalid model state");

            try
            {
                _bookService.Update(freshBook);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(this.ControllerErrorHttpResponse(e.Message));
            }

            return Ok();
        }

        public IHttpActionResult DeleteBookByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Parameter's value is empty.");

            try
            {
                _bookService.Delete(name);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(this.ControllerErrorHttpResponse(e.Message));
            }

            return Ok();
        }
    }
}