namespace API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Models;
    using BLL.Interfaces;
    using BLL.DTO;
    using AutoMapper;

    public class BooksController : ApiController
    {
        private readonly IBookStoreService _service;

        public BooksController(IBookStoreService service)
        {
            _service = service;
        }

        public IHttpActionResult PostCreateBook(BookModel newBook)
        {
            if (!ModelState.IsValid && newBook == null)
                return BadRequest("Invalid model state");

            try
            {
                _service.CreateBook(Mapper.Map<BookDto>(newBook));
            }
            catch (Exception e)
            {
                throw new HttpResponseException(InternalErrorMessage(e.Message));
            }

            return Ok();
        }

        public IHttpActionResult GetAllBooks()
        {
            IEnumerable<BookModel> books = null;

            try
            {
                books = Mapper.Map<IEnumerable<BookModel>>(_service.GetAllBooks());
            }
            catch (Exception e)
            {
                throw new HttpResponseException(InternalErrorMessage(e.Message));
            }

            return Ok(books);
        }

        public IHttpActionResult GetBookByName(string name)
        {
            if (name == null)
                return BadRequest("Parameter's value is empty.");

            BookModel book = null;

            try
            {
                book = Mapper.Map<BookModel>(_service.GetSingleBook(name));
            }
            catch (Exception e)
            {
                throw new HttpResponseException(InternalErrorMessage(e.Message));
            }

            return Ok(book);
        }

        public IHttpActionResult PutUpdateBook(BookModel freshBook)
        {
            if (!ModelState.IsValid && freshBook == null)
                return BadRequest("Invalid model state");

            try
            {
                _service.UpdateBook(Mapper.Map<BookDto>(freshBook));
            }
            catch (Exception e)
            {
                throw new HttpResponseException(InternalErrorMessage(e.Message));
            }

            return Ok();
        }

        public IHttpActionResult DeleteBookByName(string name)
        {
            if (name == null)
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