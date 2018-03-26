﻿namespace API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using API.Models;
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
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state");

            try
            {
                if (_service.GetSingleRecord<BookDto>(newBook.Name) == null)
                    throw new ArgumentException("Book with such name already exists in database.");

                _service.CreateRecord(Mapper.Map<BookDto>(newBook));
            }
            catch (Exception e)
            {
                ReturnBadRequest(e);
            }

            return Ok();
        }

        public IHttpActionResult GetAllBooks()
        {
            IEnumerable<BookModel> books = null;

            try
            {
                books = Mapper.Map<IEnumerable<BookModel>>(_service.GetAllRecords<BookDto>());
            }
            catch (Exception e)
            {
                ReturnBadRequest(e);
            }

            return Ok();
        }

        public IHttpActionResult GetBookByName(string name)
        {
            if (name == null)
                return BadRequest("Parameter's value is empty.");

            BookModel book = null;

            try
            {
                book = Mapper.Map<BookModel>(_service.GetSingleRecord<BookDto>(name));
            }
            catch (Exception e)
            {
                ReturnBadRequest(e);
            }

            return Ok(book);
        }

        public IHttpActionResult PutUpdateBook(BookModel freshBook)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state");

            try
            {
                if (_service.GetSingleRecord<BookDto>(freshBook.Name) == null)
                    throw new KeyNotFoundException("Can't find book to update.");

                _service.UpdateRecord(Mapper.Map<BookDto>(freshBook));
            }
            catch (Exception e)
            {
                ReturnBadRequest(e);
            }

            return Ok();
        }

        public IHttpActionResult DeleteBookByName(string name)
        {
            if (name == null)
                return BadRequest("Parameter's value is empty.");

            BookModel bookToDelete = null;

            try
            {
                bookToDelete = Mapper.Map<BookModel>(_service.GetSingleRecord<BookDto>(name));
                if (bookToDelete == null)
                    throw new KeyNotFoundException("Can't find book to delete.");

                _service.DeleteRecord(Mapper.Map<BookDto>(bookToDelete));
            }
            catch (Exception e)
            {
                ReturnBadRequest(e);
            }

            return Ok();
        }

        private void ReturnBadRequest(Exception e)
        {
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(e.Message),
                ReasonPhrase = "Server exception."
            });
        }
    }
}