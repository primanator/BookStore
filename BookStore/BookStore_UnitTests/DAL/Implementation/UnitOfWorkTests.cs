using DAL.EF;
using DTO.Entities;
using DAL.Implementation;
using DAL.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Data.Entity;

namespace BookStore_UnitTests.DAL.Implementation
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        [Test]
        public void Constructor_BookStoreContextPassed_Creates()
        {
            var contextFake = Substitute.For<BookStoreContext>();

            var unitOfWork = new UnitOfWork(contextFake);

            Assert.IsNotNull(unitOfWork);
        }

        [Test]
        public void Constructor_FakeContextPassed_Throws()
        {
            var contextFake = new FakeContext();

            Assert.Throws<ArgumentException>(() => new UnitOfWork(contextFake));
        }

        [Test]
        public void GetAuthorRepository_Called_Returns()
        {
            var contextFake = Substitute.For<BookStoreContext>();
            var unitOfWork = new UnitOfWork(contextFake);

            var result = unitOfWork.GetAuthorRepository();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IGenericRepository<Author>>(result);
        }

        [Test]
        public void GetBookRepository_Called_Returns()
        {
            var contextFake = Substitute.For<BookStoreContext>();
            var unitOfWork = new UnitOfWork(contextFake);

            var result = unitOfWork.GetBookRepository();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IGenericRepository<Book>>(result);
        }

        [Test]
        public void GetCountryRepository_Called_Returns()
        {
            var contextFake = Substitute.For<BookStoreContext>();
            var unitOfWork = new UnitOfWork(contextFake);

            var result = unitOfWork.GetCountryRepository();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IGenericRepository<Country>>(result);
        }

        [Test]
        public void GetGenreRepository_Called_Returns()
        {
            var contextFake = Substitute.For<BookStoreContext>();
            var unitOfWork = new UnitOfWork(contextFake);

            var result = unitOfWork.GetGenreRepository();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IGenericRepository<Genre>>(result);
        }

        [Test]
        public void GetLibraryRepository_Called_Returns()
        {
            var contextFake = Substitute.For<BookStoreContext>();
            var unitOfWork = new UnitOfWork(contextFake);

            var result = unitOfWork.GetLibraryRepository();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IGenericRepository<Library>>(result);
        }

        [Test]
        public void GetLiteratureFormRepository_Called_Returns()
        {
            var contextFake = Substitute.For<BookStoreContext>();
            var unitOfWork = new UnitOfWork(contextFake);

            var result = unitOfWork.GetLiteratureFormRepository();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IGenericRepository<LiteratureForm>>(result);
        }

        [Test]
        public void GetUserRepository_Called_Returns()
        {
            var contextFake = Substitute.For<BookStoreContext>();
            var unitOfWork = new UnitOfWork(contextFake);

            var result = unitOfWork.GetUserRepository();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IGenericRepository<User>>(result);
        }

        [Test]
        public void Save_Called_CallsContextSave()
        {
            var contextMock = Substitute.For<BookStoreContext>();
            var unitOfWork = new UnitOfWork(contextMock);

            unitOfWork.Save();

            contextMock.Received().SaveChanges();
        }
    }

    public class FakeContext : DbContext { }
}
