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
            unitOfWorkStub.GetBookRepository().FindBy(Arg.Any<Expression<Func<BookDto, bool>>>())
                .Returns(new List<BookDto>
                {
                    new BookDto()
                });
            var bookStoreService = new BookService(unitOfWorkStub);

            Assert.Throws<ArgumentException>(() => bookStoreService.Create(new BookDto()));
        }

        [Test]
        public void CreateBook_NewBook_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            List<BookDto> returnedValue = new List<BookDto>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<BookDto, bool>>>()).Returns(returnedValue);
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.Create(new BookDto());

            unitOfWorkMock.GetBookRepository().Received().Insert(Arg.Any<BookDto>());
        }

        [Test]
        public void CreateBook_NewBook_CallsUnitOfWorkSave()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            List<BookDto> returnedValue = new List<BookDto>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<BookDto, bool>>>()).Returns(returnedValue);
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.Create(new BookDto());

            unitOfWorkMock.Received().Save();
        }

        [Test]
        public void GetAllBooks_Called_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.GetAll();

            unitOfWorkMock.GetBookRepository().Received().FindBy(Arg.Any<Expression<Func<BookDto, bool>>>());
        }

        [Test]
        public void GetAllBooks_Called_Returns()
        {
            var unitOfWorkFake = Substitute.For<IUnitOfWork>();
            unitOfWorkFake.GetBookRepository().FindBy(Arg.Any<Expression<Func<BookDto, bool>>>()).Returns(new List<BookDto>());
            var bookStoreService = new BookService(unitOfWorkFake);

            var result = bookStoreService.GetAll();

            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(IEnumerable<BookDto>), result);
        }

        [Test]
        public void DeleteBook_MissingBook_Throws()
        {
            List<BookDto> returnedValue = new List<BookDto>();
            var unitOfWorkStub = Substitute.For<IUnitOfWork>();
            unitOfWorkStub.GetBookRepository().FindBy(Arg.Any<Expression<Func<BookDto, bool>>>()).Returns(returnedValue);
            var bookStoreService = new BookService(unitOfWorkStub);

            Assert.Throws<KeyNotFoundException>(() => bookStoreService.Delete("Anything"));
        }

        [Test]
        public void DeleteBook_ExistingBook_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<BookDto, bool>>>())
                .Returns(new List<BookDto>
                {
                    new BookDto()
                });
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.Delete("Anything");

            unitOfWorkMock.GetBookRepository().Received().Delete(Arg.Any<BookDto>());
        }

        [Test]
        public void DeleteBook_ExistingBook_CallsUnitOfWorkSave()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<BookDto, bool>>>())
                .Returns(new List<BookDto>
                {
                    new BookDto()
                });
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.Delete("Anything");

            unitOfWorkMock.Received().Save();
        }

        [Test]
        public void GetSingleBook_Called_Returns()
        {
            var unitOfWorkStub = Substitute.For<IUnitOfWork>();
            unitOfWorkStub.GetBookRepository().FindBy(Arg.Any<Expression<Func<BookDto, bool>>>())
                .Returns(new List<BookDto>
                {
                    new BookDto()
                });
            var bookStoreService = new BookService(unitOfWorkStub);

            var result = bookStoreService.GetSingle("Anything");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(BookDto), result);
        }

        [Test]
        public void UpdateBook_NewBook_Throws()
        {
            List<BookDto> returnedValue = new List<BookDto>();
            var unitOfWorkStub = Substitute.For<IUnitOfWork>();
            unitOfWorkStub.GetBookRepository().FindBy(Arg.Any<Expression<Func<BookDto, bool>>>()).Returns(returnedValue);
            var bookStoreService = new BookService(unitOfWorkStub);

            Assert.Throws<ArgumentException>(() => bookStoreService.Update(new BookDto()));
        }

        [Test]
        public void UpdateBook_ExistingBook_CallsRepository()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<BookDto, bool>>>())
                .Returns(new List<BookDto>
                {
                    new BookDto()
                });
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.Update(new BookDto());

            unitOfWorkMock.GetBookRepository().Received().Update(Arg.Any<BookDto>());
        }

        [Test]
        public void UpdateBook_ExistingBook_CallsUnitOfWorkSave()
        {
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            unitOfWorkMock.GetBookRepository().FindBy(Arg.Any<Expression<Func<BookDto, bool>>>())
                .Returns(new List<BookDto>
                {
                    new BookDto()
                });
            var bookStoreService = new BookService(unitOfWorkMock);

            bookStoreService.Update(new BookDto());

            unitOfWorkMock.Received().Save();
        }
    }
}
