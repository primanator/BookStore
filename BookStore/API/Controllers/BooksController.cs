using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using API.Models;

namespace API.Controllers
{
    public class BooksController : ApiController
    {
        public static IList<BookModel> books = new List<BookModel>()
        {
            new BookModel()
            {
                Id = 0,
                Name = "Murder on the Orient Express",
                Isbn = "0062073508",
                Pages = 255,
                LimitedEdition = false,
                WrittenIn = new DateTime(1936, 1, 1)
            },
            new BookModel()
            {
                Id = 1,
                Name = "The Old Man and the Sea",
                Isbn = "0684801221",
                Pages = 127,
                LimitedEdition = false,
                WrittenIn = new DateTime(1951, 1, 1)
            },
            new BookModel()
            {
                Id = 2,
                Name = "Son of the Wolf",
                Isbn = "0891906541",
                Pages = 99,
                LimitedEdition = false,
                WrittenIn = new DateTime(1911, 1, 1)
            }
        };
        public IHttpActionResult PostCreateBook(BookModel newBook)
        {
            if (!ModelState.IsValid || books.Contains(newBook))
                return BadRequest();

            try
            {
                books.Add(newBook);
                return Ok();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }

        public IHttpActionResult GetAllBooks()
        {
            return Ok(books);
        }

        public IHttpActionResult GetBookById(int id)
        {
            var book = books.FirstOrDefault(e => e.Id == id);
            if (books == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return Ok(book);
        }

        public IHttpActionResult GetBookByName(string name)
        {
            var book = books.FirstOrDefault(e => e.Name == name);
            if (books == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return Ok(book);
        }

        public IHttpActionResult PutUpdateBook(BookModel freshBook)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            BookModel oldBook = books.FirstOrDefault(b => b.Id == freshBook.Id);
            if (oldBook == null)
                return BadRequest("Not such book to update");

            try
            {
                books[books.IndexOf(oldBook)] = freshBook;
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.OK));
            }
            catch (Exception e)
            {
                return new ExceptionResult(e, this);
            }
        }

        public IHttpActionResult DeleteBookById(int id)
        {
            try
            {
                var bookToDelete = books.FirstOrDefault(r => r.Id == id);
                books.Remove(bookToDelete);
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.OK));
            }
            catch
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }
    }
}