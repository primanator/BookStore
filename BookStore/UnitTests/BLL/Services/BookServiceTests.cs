using BLL.Services;
using DAL.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DTO.Entities;

namespace UnitTests.BLL.Services
{
    [TestFixture]
    public class BookServiceTests
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
            var bookStoreService = new BookService(unitOfWorkStub);

            Assert.Throws<ArgumentException>(() => bookStoreService.Create(new Book()));
        }

        [Test]
        public void CreateBook_NewBook_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            List<Book> returnedValue = new List<Book>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>()).Returns(returnedValue);
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.Create(new Book());

            unitOfWorkMock.GetBookRepository().Received().Insert(Arg.Any<Book>());
        }

        [Test]
        public void CreateBook_NewBook_CallsUnitOfWorkSave()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            List<Book> returnedValue = new List<Book>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>()).Returns(returnedValue);
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.Create(new Book());

            unitOfWorkMock.Received().Save();
        }

        [Test]
        public void GetAllBooks_Called_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.GetAll();

            unitOfWorkMock.GetBookRepository().Received().FindBy(Arg.Any<Expression<Func<Book, bool>>>());
        }

        [Test]
        public void GetAllBooks_Called_Returns()
        {
            var unitOfWorkFake = Substitute.For<IUnitOfWork>();
            unitOfWorkFake.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book>());
            var bookStoreService = new BookService(unitOfWorkFake);

            var result = bookStoreService.GetAll();

            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(IEnumerable<Book>), result);
        }

        [Test]
        public void DeleteBook_MissingBook_Throws()
        {
            List<Book> returnedValue = new List<Book>();
            var unitOfWorkStub = Substitute.For<IUnitOfWork>();
            unitOfWorkStub.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>()).Returns(returnedValue);
            var bookStoreService = new BookService(unitOfWorkStub);

            Assert.Throws<KeyNotFoundException>(() => bookStoreService.Delete("Anything"));
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
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.Delete("Anything");

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
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.Delete("Anything");

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
            var bookStoreService = new BookService(unitOfWorkStub);

            var result = bookStoreService.GetSingle("Anything");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(Book), result);
        }

        [Test]
        public void UpdateBook_NewBook_Throws()
        {
            List<Book> returnedValue = new List<Book>();
            var unitOfWorkStub = Substitute.For<IUnitOfWork>();
            unitOfWorkStub.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>()).Returns(returnedValue);
            var bookStoreService = new BookService(unitOfWorkStub);

            Assert.Throws<ArgumentException>(() => bookStoreService.Update(new Book()));
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
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.Update(new Book());

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
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.Update(new Book());

            unitOfWorkMock.Received().Save();
        }
    }
}
