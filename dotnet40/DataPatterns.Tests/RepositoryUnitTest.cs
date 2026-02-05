using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataPatterns.Socle;
using DataPatterns.Tests.Mocks;
using DataPatterns.EntityFramework;

namespace DataPatterns.Tests
{
    [TestClass]
    public class RepositoryUnitTest
    {
        [TestMethod]
        public void Create_ShouldAddObjectToSet()
        {
            // Arrange
            var data = new List<Person>();
            var mockSet = new MockObjectSet<Person>(data);
            var mockFactory = new MockObjectSetFactory();
            mockFactory.RegisterObjectSet(mockSet);
            var repository = new Repository<Person>(mockFactory);
            var person = new Person { FirstName = "John", LastName = "Doe" };

            // Act
            repository.Create(person);

            // Assert
            Assert.AreEqual(1, data.Count);
            Assert.AreEqual(person, data[0]);
        }

        [TestMethod]
        public void ReadAll_ShouldReturnAllObjects()
        {
            // Arrange
            var person1 = new Person { FirstName = "John", LastName = "Doe" };
            var person2 = new Person { FirstName = "Jane", LastName = "Smith" };
            var data = new List<Person> { person1, person2 };
            var mockSet = new MockObjectSet<Person>(data);
            var mockFactory = new MockObjectSetFactory();
            mockFactory.RegisterObjectSet(mockSet);
            var repository = new Repository<Person>(mockFactory);

            // Act
            var result = repository.ReadAll().ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            CollectionAssert.Contains(result, person1);
            CollectionAssert.Contains(result, person2);
        }

        [TestMethod]
        public void Delete_ShouldRemoveObjectFromSet()
        {
            // Arrange
            var person = new Person { FirstName = "John", LastName = "Doe" };
            var data = new List<Person> { person };
            var mockSet = new MockObjectSet<Person>(data);
            var mockFactory = new MockObjectSetFactory();
            mockFactory.RegisterObjectSet(mockSet);
            var repository = new Repository<Person>(mockFactory);

            // Act
            repository.Delete(person);

            // Assert
            Assert.AreEqual(0, data.Count);
        }
    }
}
