using DAL.EF;
using DAL.Implementation;
using DAL.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Data.Entity;
using DTO.Entities;

namespace UnitTests.DAL.Implementation
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
        public void GetBookRepository_Called_Returns()
        {
            var contextFake = Substitute.For<BookStoreContext>();
            var unitOfWork = new UnitOfWork(contextFake);

            var result = unitOfWork.BookRepository;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IRepository<BookDto>>(result);
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
