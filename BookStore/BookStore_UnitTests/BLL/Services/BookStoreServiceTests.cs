using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BookStore_UnitTests.BLL.Services
{
    [TestFixture]
    public class BookStoreServiceTests
    {
        [Test]
        public void CreateBook_ExistingBook_Throws()
        {
            var bookStoreServiceFake = Substitute.For<IBookStoreService>();
            var returnValue = new BookDto();
            bookStoreServiceFake.When(service => service.CreateBook(Arg.Is<BookDto>(book => book != null)))
                .Do(callinfo =>
                {
                    if (bookStoreServiceFake.GetSingleBook(((BookDto)callinfo[0]).Name) != null)
                        throw new ArgumentException("Database already contains book with such name.");
                });
            bookStoreServiceFake.GetSingleBook("").ReturnsForAnyArgs(returnValue);

            Assert.Throws<ArgumentException>(() => bookStoreServiceFake.CreateBook(new BookDto()));
        }

        [Test]
        public void CreateBook_NewBook_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var bookStoreServiceFake = Substitute.For<IBookStoreService>();
            BookDto returnValue = null;
            bookStoreServiceFake.When(service => service.CreateBook(Arg.Is<BookDto>(book => book != null)))
                .Do(callinfo =>
                {
                    if (bookStoreServiceFake.GetSingleBook(((BookDto)callinfo[0]).Name) != null)
                        throw new ArgumentException("Database already contains book with such name.");
                    unitOfWorkMock.GetBookRepository().Insert(Arg.Any<Book>());
                });
            bookStoreServiceFake.GetSingleBook("").ReturnsForAnyArgs(returnValue);

            bookStoreServiceFake.CreateBook(new BookDto());

            unitOfWorkMock.GetBookRepository().Received();
        }

        public void CreateBook_NewBook_CallsUnitOfWorkSave()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var bookStoreServiceFake = Substitute.For<IBookStoreService>();
            BookDto returnValue = null;
            bookStoreServiceFake.When(service => service.CreateBook(Arg.Is<BookDto>(book => book != null)))
                .Do(callinfo =>
                {
                    if (bookStoreServiceFake.GetSingleBook(((BookDto)callinfo[0]).Name) != null)
                        throw new ArgumentException("Database already contains book with such name.");
                    unitOfWorkMock.GetBookRepository().Insert(Arg.Any<Book>());
                    unitOfWorkMock.Save();
                });
            bookStoreServiceFake.GetSingleBook("").ReturnsForAnyArgs(returnValue);

            bookStoreServiceFake.CreateBook(new BookDto());

            unitOfWorkMock.Received().Save();
        }

        [Test]
        public void GetAllBooks_Called_CallRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var bookStoreServiceFake = Substitute.For<IBookStoreService>();
            bookStoreServiceFake.When(service => service.GetAllBooks())
                .Do(callback =>
                {
                    unitOfWorkMock.GetBookRepository().FindBy(b => true);
                });

            bookStoreServiceFake.GetAllBooks();

            unitOfWorkMock.Received().GetBookRepository();
        }

        [Test]
        public void GetAllBooks_Called_Returns()
        {
            var bookStoreServiceFake = Substitute.For<IBookStoreService>();
            bookStoreServiceFake.GetAllBooks().ReturnsForAnyArgs(new List<BookDto>());

            var result = bookStoreServiceFake.GetAllBooks();

            Assert.NotNull(result);
        }

        [Test]
        public void DeleteBook_MissingBook_Throws()
        {
            var bookStoreServiceFake = Substitute.For<IBookStoreService>();
            BookDto returnValue = null;
            bookStoreServiceFake.When(service => service.DeleteBook(Arg.Is<string>(s => !string.IsNullOrEmpty(s))))
                .Do(callinfo =>
                {
                    if (bookStoreServiceFake.GetSingleBook((string)callinfo[0]) == null)
                        throw new KeyNotFoundException("Database does not contain such book to delete.");
                });
            bookStoreServiceFake.GetSingleBook("").ReturnsForAnyArgs(returnValue);

            Assert.Throws<KeyNotFoundException>(() => bookStoreServiceFake.DeleteBook("Anything"));
        }

        [Test]
        public void DeleteBook_ExistingBook_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var bookStoreServiceFake = Substitute.For<IBookStoreService>();
            var returnValue = new BookDto();
            bookStoreServiceFake.When(service => service.DeleteBook(Arg.Is<string>(s => !string.IsNullOrEmpty(s))))
                .Do(callinfo =>
                {
                    if (bookStoreServiceFake.GetSingleBook((string)callinfo[0]) == null)
                        throw new KeyNotFoundException("Database does not contain such book to delete.");
                    unitOfWorkMock.GetBookRepository().Delete(Arg.Any<Book>());
                });
            bookStoreServiceFake.GetSingleBook("").ReturnsForAnyArgs(returnValue);

            bookStoreServiceFake.DeleteBook("Anything");

            unitOfWorkMock.GetBookRepository().Received();
        }

        [Test]
        public void DeleteBook_ExistingBook_CallsUnitOfWorkSave()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var bookStoreServiceFake = Substitute.For<IBookStoreService>();
            var returnValue = new BookDto();
            bookStoreServiceFake.When(service => service.DeleteBook(Arg.Is<string>(s => !string.IsNullOrEmpty(s))))
                .Do(callinfo =>
                {
                    if (bookStoreServiceFake.GetSingleBook((string)callinfo[0]) == null)
                        throw new KeyNotFoundException("Database does not contain such book to delete.");
                    unitOfWorkMock.GetBookRepository().Delete(Arg.Any<Book>());
                    unitOfWorkMock.Save();
                });
            bookStoreServiceFake.GetSingleBook("").ReturnsForAnyArgs(returnValue);

            bookStoreServiceFake.DeleteBook("Anything");

            unitOfWorkMock.Received().Save();
        }

        [Test]
        public void GetSingleBook_Called_Returns()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var bookStoreServiceFake = Substitute.For<IBookStoreService>();
            var returnValue = default(BookDto);
            bookStoreServiceFake.GetSingleBook(Arg.Is<string>(s => !string.IsNullOrEmpty(s))).Returns(default(BookDto));
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<Book, bool>>>()).Returns(default(List<Book>));

            var result = bookStoreServiceFake.GetSingleBook("Anything");

            Assert.AreEqual(returnValue, result);
        }

        [Test]
        public void UpdateBook_NewBook_Throws()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var bookStoreServiceFake = Substitute.For<IBookStoreService>();
            BookDto returnValue = null;
            bookStoreServiceFake.When(service => service.UpdateBook(Arg.Is<BookDto>(book => book != null)))
                .Do(callinfo =>
                {
                    if (bookStoreServiceFake.GetSingleBook(((BookDto)callinfo[0]).Name) == null)
                        throw new KeyNotFoundException("Database does not contain such book to delete.");
                });
            bookStoreServiceFake.GetSingleBook("").ReturnsForAnyArgs(returnValue);

            Assert.Throws<KeyNotFoundException>(() => bookStoreServiceFake.UpdateBook(new BookDto()));
        }

        [Test]
        public void UpdateBook_ExistingBook_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var bookStoreServiceFake = Substitute.For<IBookStoreService>();
            var returnValue = new BookDto();
            bookStoreServiceFake.When(service => service.UpdateBook(Arg.Is<BookDto>(book => book != null)))
                .Do(callinfo =>
                {
                    if (bookStoreServiceFake.GetSingleBook(((BookDto)callinfo[0]).Name) == null)
                        throw new KeyNotFoundException("Database does not contain such book to delete.");
                    unitOfWorkMock.GetBookRepository().Update(Arg.Any<Book>());
                });
            bookStoreServiceFake.GetSingleBook("").ReturnsForAnyArgs(returnValue);

            bookStoreServiceFake.UpdateBook(new BookDto());

            unitOfWorkMock.GetBookRepository().Received();
        }

        [Test]
        public void UpdateBook_ExistingBook_CallsSave()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var bookStoreServiceFake = Substitute.For<IBookStoreService>();
            var returnValue = new BookDto();
            bookStoreServiceFake.When(service => service.UpdateBook(Arg.Is<BookDto>(book => book != null)))
                .Do(callinfo =>
                {
                    if (bookStoreServiceFake.GetSingleBook(((BookDto)callinfo[0]).Name) == null)
                        throw new KeyNotFoundException("Database does not contain such book to delete.");
                    unitOfWorkMock.GetBookRepository().Update(Arg.Any<Book>());
                    unitOfWorkMock.Save();
                });
            bookStoreServiceFake.GetSingleBook("").ReturnsForAnyArgs(returnValue);

            bookStoreServiceFake.UpdateBook(new BookDto());

            unitOfWorkMock.Received().Save();
        }
    }
}
