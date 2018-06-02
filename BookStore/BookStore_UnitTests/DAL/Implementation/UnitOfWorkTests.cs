using DAL.EF;
using DAL.Entities;
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
            var contextStub = Substitute.For<BookStoreContext>();

            var unitOfWorkFake = new UnitOfWorkFake(contextStub);

            Assert.IsNotNull(unitOfWorkFake);
        }

        [Test]
        public void GetAuthorRepository_Called_Returns()
        {
            var unitOfWorkFake = Substitute.For<IUnitOfWork>();
            var authorRepo = Substitute.For<IGenericRepository<Author>>();
            unitOfWorkFake.GetAuthorRepository().Returns(callinfo => authorRepo);

            var result = unitOfWorkFake.GetAuthorRepository();

            Assert.IsInstanceOf<IGenericRepository<Author>>(result);
        }

        [Test]
        public void GetBookRepository_Called_Returns()
        {
            var unitOfWorkFake = Substitute.For<IUnitOfWork>();
            var bookRepo = Substitute.For<IGenericRepository<Book>>();
            unitOfWorkFake.GetBookRepository().Returns(callinfo => bookRepo);

            var result = unitOfWorkFake.GetBookRepository();

            Assert.IsInstanceOf<IGenericRepository<Book>>(result);
        }

        [Test]
        public void GetCountryRepository_Called_Returns()
        {
            var unitOfWorkFake = Substitute.For<IUnitOfWork>();
            var countryRepo = Substitute.For<IGenericRepository<Country>>();
            unitOfWorkFake.GetCountryRepository().Returns(callinfo => countryRepo);

            var result = unitOfWorkFake.GetCountryRepository();

            Assert.IsInstanceOf<IGenericRepository<Country>>(result);
        }

        [Test]
        public void GetGenreRepository_Called_Returns()
        {
            var unitOfWorkFake = Substitute.For<IUnitOfWork>();
            var genreRepo = Substitute.For<IGenericRepository<Genre>>();
            unitOfWorkFake.GetGenreRepository().Returns(callinfo => genreRepo);

            var result = unitOfWorkFake.GetGenreRepository();

            Assert.IsInstanceOf<IGenericRepository<Genre>>(result);
        }

        [Test]
        public void GetLibraryRepository_Called_Returns()
        {
            var unitOfWorkFake = Substitute.For<IUnitOfWork>();
            var libraryRepo = Substitute.For<IGenericRepository<Library>>();
            unitOfWorkFake.GetLibraryRepository().Returns(callinfo => libraryRepo);

            var result = unitOfWorkFake.GetLibraryRepository();

            Assert.IsInstanceOf<IGenericRepository<Library>>(result);
        }

        [Test]
        public void GetLiteratureFormRepository_Called_Returns()
        {
            var unitOfWorkFake = Substitute.For<IUnitOfWork>();
            var literatureFormRepo = Substitute.For<IGenericRepository<LiteratureForm>>();
            unitOfWorkFake.GetLiteratureFormRepository().Returns(callinfo => literatureFormRepo);

            var result = unitOfWorkFake.GetLiteratureFormRepository();

            Assert.IsInstanceOf<IGenericRepository<LiteratureForm>>(result);
        }

        [Test]
        public void GetUserRepository_Called_Returns()
        {
            var unitOfWorkFake = Substitute.For<IUnitOfWork>();
            var userRepo = Substitute.For<IGenericRepository<User>>();
            unitOfWorkFake.GetUserRepository().Returns(callinfo => userRepo);

            var result = unitOfWorkFake.GetUserRepository();

            Assert.IsInstanceOf<IGenericRepository<User>>(result);
        }

        [Test]
        public void Save_Called_CallsContextSave()
        {
            var contextMock = Substitute.For<BookStoreContext>();
            var unitOfWorkFake = new UnitOfWorkFake(contextMock);

            unitOfWorkFake.Save();

            contextMock.Received().SaveChanges();
        }
    }

    public class UnitOfWorkFake : UnitOfWork
    {
        public new BookStoreContext context;

        public UnitOfWorkFake(DbContext context) : base (context)
        {
            this.context = (BookStoreContext)context;
        }
    }

}
