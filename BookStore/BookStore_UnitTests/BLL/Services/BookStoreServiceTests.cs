using BLL.Services;
using DAL.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DTO.Entities;

namespace BookStore_UnitTests.BLL.Services
{
    [TestFixture]
    public class BookStoreServiceTests
    {
        [Test]
        public void CreateBook_ExistingBook_Throws()
        {
            var unitOfWorkStub = Substitute.For<IUnitOfWork>();
            unitOfWorkStub.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>())
                .Returns(new List<Book>
                {
                    new Book()
                });
            var bookStoreService = new BookStoreService(unitOfWorkStub);

            Assert.Throws<ArgumentException>(() => bookStoreService.CreateBook(new Book()));
        }

        [Test]
        public void CreateBook_NewBook_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            List<Book> returnedValue = new List<Book>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>()).Returns(returnedValue);
            var bookStoreService = new BookStoreService(unitOfWorkMock);

            bookStoreService.CreateBook(new Book());

            unitOfWorkMock.GetBookRepository().Received().Insert(Arg.Any<Book>());
        }

        [Test]
        public void CreateBook_NewBook_CallsUnitOfWorkSave()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            List<Book> returnedValue = new List<Book>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>()).Returns(returnedValue);
            var bookStoreService = new BookStoreService(unitOfWorkMock);

            bookStoreService.CreateBook(new Book());

            unitOfWorkMock.Received().Save();
        }

        [Test]
        public void GetAllBooks_Called_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var bookStoreService = new BookStoreService(unitOfWorkMock);

            bookStoreService.GetAllBooks();

            unitOfWorkMock.GetBookRepository().Received().FindBy(Arg.Any<Expression<Func<Book, bool>>>());
        }

        [Test]
        public void GetAllBooks_Called_Returns()
        {
            var unitOfWorkFake = Substitute.For<IUnitOfWork>();
            unitOfWorkFake.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book>());
            var bookStoreService = new BookStoreService(unitOfWorkFake);

            var result = bookStoreService.GetAllBooks();

            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(IEnumerable<Book>), result);
        }

        [Test]
        public void DeleteBook_MissingBook_Throws()
        {
            List<Book> returnedValue = new List<Book>();
            var unitOfWorkStub = Substitute.For<IUnitOfWork>();
            unitOfWorkStub.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>()).Returns(returnedValue);
            var bookStoreService = new BookStoreService(unitOfWorkStub);

            Assert.Throws<KeyNotFoundException>(() => bookStoreService.DeleteBook("Anything"));
        }

        [Test]
        public void DeleteBook_ExistingBook_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>())
                .Returns(new List<Book>
                {
                    new Book()
                });
            var bookStoreService = new BookStoreService(unitOfWorkMock);

            bookStoreService.DeleteBook("Anything");

            unitOfWorkMock.GetBookRepository().Received().Delete(Arg.Any<Book>());
        }

        [Test]
        public void DeleteBook_ExistingBook_CallsUnitOfWorkSave()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>())
                .Returns(new List<Book>
                {
                    new Book()
                });
            var bookStoreService = new BookStoreService(unitOfWorkMock);

            bookStoreService.DeleteBook("Anything");

            unitOfWorkMock.Received().Save();
        }

        [Test]
        public void GetSingleBook_Called_Returns()
        {
            var unitOfWorkStub = Substitute.For<IUnitOfWork>();
            unitOfWorkStub.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>())
                .Returns(new List<Book>
                {
                    new Book()
                });
            var bookStoreService = new BookStoreService(unitOfWorkStub);

            var result = bookStoreService.GetSingleBook("Anything");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(Book), result);
        }

        [Test]
        public void UpdateBook_NewBook_Throws()
        {
            List<Book> returnedValue = new List<Book>();
            var unitOfWorkStub = Substitute.For<IUnitOfWork>();
            unitOfWorkStub.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>()).Returns(returnedValue);
            var bookStoreService = new BookStoreService(unitOfWorkStub);

            Assert.Throws<ArgumentException>(() => bookStoreService.UpdateBook(new Book()));
        }

        [Test]
        public void UpdateBook_ExistingBook_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>())
                .Returns(new List<Book>
                {
                    new Book()
                });
            var bookStoreService = new BookStoreService(unitOfWorkMock);

            bookStoreService.UpdateBook(new Book());

            unitOfWorkMock.GetBookRepository().Received().Update(Arg.Any<Book>());
        }

        [Test]
        public void UpdateBook_ExistingBook_CallsUnitOfWorkSave()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>())
                .Returns(new List<Book>
                {
                    new Book()
                });
            var bookStoreService = new BookStoreService(unitOfWorkMock);

            bookStoreService.UpdateBook(new Book());

            unitOfWorkMock.Received().Save();
        }
    }
}
