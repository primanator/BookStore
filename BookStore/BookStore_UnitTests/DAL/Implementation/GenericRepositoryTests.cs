using DAL.Entities;
using DAL.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BookStore_UnitTests.DAL.Implementation
{
    [TestFixture]
    public class GenericRepositoryTests
    {
        [Test]
        public void Get_IdLessThanZero_Throws()
        {
            var repoFake = Substitute.For<IGenericRepository<Entity>>();
            repoFake.Get(Arg.Is<int>(x => x < 0)).Returns(callinfo => { throw new ArgumentOutOfRangeException("Entity Id is less or equals zero."); });

            Assert.Throws<ArgumentOutOfRangeException>(() => repoFake.Get(-10));
        }

        [TestCase(0)]
        [TestCase(4)]
        public void Get_MissingKey_Throws(int missingKey)
        {
            var repoFake = new FakeRepository<Book>
            {
                entities = new Dictionary<int, Book>()
                {
                    {1, new Book() },
                    {2, new Book() },
                    {3, new Book() }
                },
                throwNotFound = new InvalidOperationException("was not found in the DB")
            };

            Assert.Throws<InvalidOperationException>(() => repoFake.Get(missingKey));
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Get_PresentKey_Returns(int presentKey)
        {
            var repoFake = new FakeRepository<Book>
            {
                entities = new Dictionary<int, Book>()
                {
                    {1, new Book() },
                    {2, new Book() },
                    {3, new Book() }
                }
            };

            var result = repoFake.Get(presentKey);

            Assert.NotNull(result);
        }

        [Test]
        public void Delete_PresentEntry_Removes()
        {
            var presentEntry = new Book();
            var repoFake = new FakeRepository<Book>
            {
                entities = new Dictionary<int, Book>()
                {
                    {1, presentEntry },
                    {2, new Book() },
                    {3, new Book() }
                },
                throwNotFound = new InvalidOperationException("was not found in the DB")
            };

            repoFake.Delete(presentEntry);

            Assert.Throws<InvalidOperationException>(() => repoFake.Get(1));
        }

        [Test]
        public void FindBy_PredicatePassed_Returns()
        {
            var repoFake = Substitute.For<IGenericRepository<Entity>>();
            repoFake.FindBy(Arg.Any<Expression<Func<Entity, bool>>>()).Returns(callinfo => new List<Entity>());

            var result = repoFake.FindBy(entry => entry.Name == "Any");

            Assert.NotNull(result);
        }

        [Test]
        public void Insert_ObjectPassed_AddsNew()
        {
            var repoFake = new FakeRepository<Book>
            {
                entities = new Dictionary<int, Book>()
                {
                    {1, new Book() },
                    {2, new Book() },
                    {3, new Book() }
                },
            };

            repoFake.Insert(new Book());

            var result = repoFake.Get(repoFake.entities.Count);
            Assert.NotNull(result);
        }

        [Test]
        public void Update_FoundableObjectPassed_Updates()
        {
            var oldBook = new Book
            {
                Name = "Book for Dummies"
            };
            var newBook = new Book
            {
                Name = "Book for Smarties"
            };
            var repoFake = new FakeRepository<Book>
            {
                entities = new Dictionary<int, Book>()
                {
                    {1, oldBook },
                    {2, new Book() },
                    {3, new Book() }
                },
            };

            repoFake.Update(newBook);

            var result = repoFake.Get(1);
            Assert.AreEqual(result, newBook);
        }
    }

    public class FakeRepository<T> : IGenericRepository<T> where T : Entity
    {
        public Exception throwNotFound;
        public Dictionary<int, T> entities;
        public T toUpdate;

        public T Get(int id)
        {
            if (!entities.ContainsKey(id))
                throw throwNotFound;
            return entities[id];
        }

        public void Delete(T entity)
        {
            var toDelete = entities.Where(entry => entry.Value == entity).SingleOrDefault();
            entities.Remove(toDelete.Key);
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate) { throw new NotImplementedException(); }

        public void Insert(T entity)
        {
            entities.Add(entities.Count + 1, entity);
        }

        public void Update(T entity)
        {
            entities[1] = entity;
        }
    }
}
