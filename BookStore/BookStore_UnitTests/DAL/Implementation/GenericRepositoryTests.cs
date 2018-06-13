using DAL.EF;
using DAL.Entities;
using DAL.Implementation;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace BookStore_UnitTests.DAL.Implementation
{
    [TestFixture]
    public class GenericRepositoryTests
    {
        [TestCase(0)]
        [TestCase(-10)]
        public void Get_IdLessThanOrEqualsZero_Throws(int id)
        {
            var contextFake = Substitute.For<BookStoreContext>();
            var repository = new GenericRepository<Book>(contextFake);

            Assert.Throws<ArgumentOutOfRangeException>(() => repository.Get(id));
        }

        [TestCase(1)]
        [TestCase(4)]
        public void Get_MissingEntry_Throws(int missingKey)
        {
            var dbSetStub = Substitute.For<DbSet<Book>>();
            dbSetStub.Find(Arg.Any<int>()).Returns(default(Book));
            var contextStub = Substitute.For<BookStoreContext>();
            contextStub.Set<Book>().Returns(dbSetStub);
            var repository = new GenericRepository<Book>(contextStub);

            Assert.Throws<InvalidOperationException>(() => repository.Get(missingKey));
        }

        [TestCase(2)]
        [TestCase(3)]
        public void Get_PresentKey_Returns(int presentKey)
        {
            var dbSetStub = Substitute.For<DbSet<Book>>();
            dbSetStub.Find(Arg.Any<int>()).Returns(new Book());
            var contextStub = Substitute.For<BookStoreContext>();
            contextStub.Set<Book>().Returns(dbSetStub);
            var repository = new GenericRepository<Book>(contextStub);

            var result = repository.Get(presentKey);

            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(Book), result);
        }

        [Test]
        public void Delete_ObjectPassed_ChangesEntryStateToDeleted()
        {
            var dbSetStub = Substitute.For<DbSet<Book>>();
            var returnedValue = new Book();
            dbSetStub.Find(Arg.Any<int>()).Returns(returnedValue);
            var contextStub = Substitute.For<BookStoreContext>();
            contextStub.Set<Book>().Returns(dbSetStub);
            var repository = new GenericRepository<Book>(contextStub);

            repository.Delete(returnedValue);
            var resultState = contextStub.Entry(dbSetStub.Find(1)).State;

            Assert.AreEqual(resultState, EntityState.Deleted);
        }

        [Test]
        public void Insert_ObjectPassed_CallsContextSetAdd()
        {
            var contextMock = Substitute.For<BookStoreContext>();
            var dbSetStub = Substitute.For<DbSet<Book>>();
            contextMock.Set<Book>().Returns(dbSetStub);
            var repository = new GenericRepository<Book>(contextMock);

            repository.Insert(new Book());

            contextMock.Set<Book>().Received().Add(Arg.Any<Book>());
        }

        [Test]
        public void Update_ObjectPassed_CallsContextSetAttach()
        {
            var contextMock = Substitute.For<BookStoreContext>();
            var dbSetStub = Substitute.For<DbSet<Book>>();
            contextMock.Set<Book>().Returns(dbSetStub);
            var repository = new GenericRepository<Book>(contextMock);

            repository.Update(new Book());

            contextMock.Set<Book>().Received().Attach(Arg.Any<Book>());
        }

        [Test]
        public void Update_ObjectPassed_ChangesEntryStateToModified()
        {
            var bookToUpdate = new Book();
            var contextStub = Substitute.For<BookStoreContext>();
            var dbSetStub = Substitute.For<DbSet<Book>>();
            contextStub.Set<Book>().Returns(dbSetStub);
            var repository = new GenericRepository<Book>(contextStub);

            repository.Update(bookToUpdate);
            var resultState = contextStub.Entry(bookToUpdate).State;

            Assert.AreEqual(resultState, EntityState.Modified);
        }

        [Test]
        public void FindBy_PredicatePassed_Returns()
        {
            var data = new List<Book>
            {
                new Book { Name = "BBB" },
                new Book { Name = "ZZZ" },
                new Book { Name = "AAA" }
            }.AsQueryable();

            var dbSetStub = NSubstituteUtils.CreateMockDbSet(data);
            dbSetStub.AsNoTracking().Returns(dbSetStub);

            var contextStub = Substitute.For<BookStoreContext>();
            contextStub.Set<Book>().Returns(dbSetStub);
            var repository = new GenericRepository<Book>(contextStub);

            var result = repository.FindBy(entry => entry.Name == "ZZZ");

            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(IEnumerable<Book>), result);
        }

        [Test]
        public void FindBy_PredicatePassed_CallsContextAsNoTracking()
        {
            var data = new List<Book>
            {
                new Book { Name = "BBB" },
                new Book { Name = "ZZZ" },
                new Book { Name = "AAA" }
            }.AsQueryable();

            var dbSetMock = NSubstituteUtils.CreateMockDbSet(data);
            dbSetMock.AsNoTracking().Returns(dbSetMock);

            var contextStub = Substitute.For<BookStoreContext>();
            contextStub.Set<Book>().Returns(dbSetMock);
            var repository = new GenericRepository<Book>(contextStub);

            var result = repository.FindBy(entry => entry.Name == "ZZZ");

            dbSetMock.Received().AsNoTracking();
        }
    }

    public static class NSubstituteUtils
    {
        public static DbSet<T> CreateMockDbSet<T>(IEnumerable<T> data = null) where T : class
        {
            var dbSetMock = Substitute.For<DbSet<T>, IQueryable<T>>();

            if (data != null)
            {
                var queryable = data.AsQueryable();

                ((IQueryable<T>)dbSetMock).Provider.Returns(queryable.Provider);
                ((IQueryable<T>)dbSetMock).Expression.Returns(queryable.Expression);
                ((IQueryable<T>)dbSetMock).ElementType.Returns(queryable.ElementType);
                ((IQueryable<T>)dbSetMock).GetEnumerator().Returns(queryable.GetEnumerator());
            }

            return dbSetMock;
        }
    }
}