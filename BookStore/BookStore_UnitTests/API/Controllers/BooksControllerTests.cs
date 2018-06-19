using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using API.Controllers;
using BLL.Interfaces;
using NSubstitute;
using NUnit.Framework;
using DTO.Entities;

namespace BookStore_UnitTests.API.Controllers
{
    [TestFixture]
    public class BooksControllerTests
    {

        [Test]
        public void PostCreateBook_NewBookIsNull_BadRequest()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceStub);

            var result = booksController.PostCreateBook(null);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void PostCreateBook_NewBook_CallsService()
        {
            var serviceMock = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceMock);

            booksController.PostCreateBook(new Book());

            serviceMock.Received().CreateBook(Arg.Any<Book>());
        }

        [Test]
        public void PostCreateBook_ServiceThrows_Throws()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            serviceStub.When(service => service.CreateBook(Arg.Any<Book>()))
                .Do(callback =>
                {
                    throw new Exception();
                });
            var booksController = new BooksController(serviceStub);

            Assert.Throws<HttpResponseException>(() => booksController.PostCreateBook(new Book()));
        }

        [Test]
        public void PostCreateBook_NewBook_OkResult()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceStub);

            var result = booksController.PostCreateBook(new Book());

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void GetAllBooks_Called_CallsService()
        {
            var serviceMock = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceMock);

            booksController.GetAllBooks();

            serviceMock.Received().GetAllBooks();
        }

        [Test]
        public void GetAllBooks_ServiceThrows_Throws()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            serviceStub.When(service => service.GetAllBooks())
                .Do(callback =>
                {
                    throw new Exception();
                });
            var booksController = new BooksController(serviceStub);

            Assert.Throws<HttpResponseException>(() => booksController.GetAllBooks());
        }

        [Test]
        public void GetAllBooks_Called_OkResultWithEnumerable()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceStub);

            var result = booksController.GetAllBooks();

            Assert.IsInstanceOf<OkNegotiatedContentResult<IEnumerable<Book>>>(result);
        }

        [Test]
        public void GetBookByName_NullPassed_BadRequest()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceStub);

            var result = booksController.GetBookByName(null);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void GetBookByName_EmptyStringPassed_BadRequest()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceStub);

            var result = booksController.GetBookByName("");

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void GetBookByName_CorrectStringPassed_CallsService()
        {
            var serviceMock = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceMock);

            booksController.GetBookByName("AnyBookName");

            serviceMock.Received().GetSingleBook(Arg.Any<string>());
        }

        [Test]
        public void GetBookByName_ServiceThrows_Throws()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            serviceStub.When(service => service.GetSingleBook(Arg.Any<string>()))
                .Do(callback =>
                {
                    throw new Exception();
                });
            var booksController = new BooksController(serviceStub);

            Assert.Throws<HttpResponseException>(() => booksController.GetBookByName("AnyBookName"));
        }

        [Test]
        public void GetBookByName_CorrectStringPassed_OkResultWithValue()
        {
            var serviceMock = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceMock);

            var result = booksController.GetBookByName("AnyBookName");

            Assert.IsInstanceOf<OkNegotiatedContentResult<Book>>(result);
        }

        [Test]
        public void PutUpdateBook_BookIsNull_BadRequest()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceStub);

            var result = booksController.PutUpdateBook(null);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void PutUpdateBook_BookPassed_CallsService()
        {
            var serviceMock = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceMock);

            booksController.PutUpdateBook(new Book());

            serviceMock.Received().UpdateBook(Arg.Any<Book>());
        }

        [Test]
        public void PutUpdateBook_ServiceThrows_Throws()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            serviceStub.When(service => service.UpdateBook(Arg.Any<Book>()))
                .Do(callback =>
                {
                    throw new Exception();
                });
            var booksController = new BooksController(serviceStub);

            Assert.Throws<HttpResponseException>(() => booksController.PutUpdateBook(new Book()));
        }

        [Test]
        public void PutUpdateBook_BookPassed_OkResult()
        {
            var serviceMock = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceMock);

            var result = booksController.PutUpdateBook(new Book());

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void DeleteBookByName_NullPassed_BadRequest()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceStub);

            var result = booksController.DeleteBookByName(null);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void DeleteBookByName_EmptyStringPassed_BadRequest()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceStub);

            var result = booksController.DeleteBookByName("");

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void DeleteBookByName_CorrectStringPassed_CallsService()
        {
            var serviceMock = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceMock);

            booksController.DeleteBookByName("AnyBookName");

            serviceMock.Received().DeleteBook(Arg.Any<string>());
        }

        [Test]
        public void DeleteBookByName_ServiceThrows_Throws()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            serviceStub.When(service => service.DeleteBook(Arg.Any<string>()))
                .Do(callback =>
                {
                    throw new Exception();
                });
            var booksController = new BooksController(serviceStub);

            Assert.Throws<HttpResponseException>(() => booksController.DeleteBookByName("AnyBookName"));
        }

        [Test]
        public void DeleteBookByName_CorrectStringPassed_OkResult()
        {
            var serviceStub = Substitute.For<IBookStoreService>();
            var booksController = new BooksController(serviceStub);

            var result = booksController.DeleteBookByName("AnyBookName");

            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
