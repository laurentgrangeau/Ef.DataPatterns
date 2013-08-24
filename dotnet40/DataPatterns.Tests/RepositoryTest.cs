using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataPatterns.Socle;
using DataPatterns.EntityFramework;
using DataPatterns.Socle.Builders;
using DataPatterns.Socle.Adapters;
using DataPatterns.Socle.Factories;

namespace DataPatterns.Tests
{
    /// <summary>
    /// Description résumée pour RepositoryTest
    /// </summary>
    [TestClass]
    public class RepositoryTest
    {
        public RepositoryTest()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
        }

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void AddPersonTest()
        {
            using (var context = ContextAdapterFactory.CreateAdapter(new AdventureWorks2008R2Entities()))
            {
                using (var unitOfWork = new UnitOfWork(context.CreateObjectContext()))
                {
                    var personRepo = new Repository<Person>(context);
                    var businessEntityRepo = new Repository<BusinessEntity>(context);

                    var businessEntity = new BusinessEntity()
                    {
                        rowguid = Guid.NewGuid(),
                        ModifiedDate = DateTime.Now
                    };

                    var person = new Person()
                    {
                        BusinessEntity = businessEntity,
                        FirstName = "Laurent",
                        LastName = "Grangeau",
                        PersonType = "GC",
                        rowguid = Guid.NewGuid(),
                        ModifiedDate = DateTime.Now
                    };

                    businessEntityRepo.Create(businessEntity);
                    personRepo.Create(person);

                    unitOfWork.Commit();
                }
            }
        }

        [TestMethod]
        public void UpdatePersonTest()
        {
            var dateNow = DateTime.Now;

            using (var context = ContextAdapterFactory.CreateAdapter(new AdventureWorks2008R2Entities()))
            {
                using (var unitOfWork = new UnitOfWork(context.CreateObjectContext()))
                {
                    // Arrange
                    var personRepo = new Repository<Person>(context);

                    var predicate = PredicateBuilder.True<Person>();
                    predicate = predicate.And(p => p.LastName.Equals("Rizzi"));
                    predicate = predicate.And(p => p.BusinessEntityID == 1695);

                    var person = personRepo.Single(predicate, p => p.BusinessEntity);

                    person.ModifiedDate = dateNow;
                    person.BusinessEntity.ModifiedDate = dateNow;

                    // Act
                    personRepo.Update(person);
                    unitOfWork.Commit();
                }
            }

            using (var context = ContextAdapterFactory.CreateAdapter(new AdventureWorks2008R2Entities()))
            {
                using (var unitOfWork = new UnitOfWork(context.CreateObjectContext()))
                {
                    // Arrange
                    var personRepo = new Repository<Person>(context);

                    var predicate = PredicateBuilder.True<Person>();
                    predicate = predicate.And(p => p.LastName.Equals("Rizzi"));
                    predicate = predicate.And(p => p.BusinessEntityID == 1695);

                    // Act
                    var person = personRepo.Single(predicate, p => p.BusinessEntity);

                    // Assert
                    Assert.IsNotNull(person);
                    Assert.AreEqual<DateTime>(dateNow, person.ModifiedDate);
                    Assert.AreEqual<DateTime>(dateNow, person.BusinessEntity.ModifiedDate);
                }
            }
        }

        [TestMethod]
        public void UpdatePersonWithNewBusinessEntityTest()
        {
            var dateNow = DateTime.Now;

            using (var context = ContextAdapterFactory.CreateAdapter(new AdventureWorks2008R2Entities()))
            {
                using (var unitOfWork = new UnitOfWork(context.CreateObjectContext()))
                {
                    // Arrange
                    var personRepo = new Repository<Person>(context);

                    var predicate = PredicateBuilder.True<Person>();
                    predicate = predicate.And(p => p.LastName.Equals("Rizzi"));
                    predicate = predicate.And(p => p.BusinessEntityID == 1695);

                    var person = personRepo.Single(predicate, p => p.BusinessEntity);

                    person.ModifiedDate = dateNow;
                    person.BusinessEntity = new BusinessEntity { ModifiedDate = dateNow };

                    // Act
                    personRepo.Update(person);
                    unitOfWork.Commit();
                }
            }

            using (var context = ContextAdapterFactory.CreateAdapter(new AdventureWorks2008R2Entities()))
            {
                using (var unitOfWork = new UnitOfWork(context.CreateObjectContext()))
                {
                    // Arrange
                    var personRepo = new Repository<Person>(context);

                    var predicate = PredicateBuilder.True<Person>();
                    predicate = predicate.And(p => p.LastName.Equals("Rizzi"));
                    predicate = predicate.And(p => p.BusinessEntityID == 1695);

                    // Act
                    var person = personRepo.Single(predicate, p => p.BusinessEntity);

                    // Assert
                    Assert.IsNotNull(person);
                    Assert.AreEqual<DateTime>(dateNow, person.ModifiedDate);
                    Assert.AreEqual<DateTime>(dateNow, person.BusinessEntity.ModifiedDate);
                }
            }
        }

        [TestMethod]
        public void SinglePersonTest()
        {
            using (var context = ContextAdapterFactory.CreateAdapter(new AdventureWorks2008R2Entities()))
            {
                var personRepo = new Repository<Person>(context);

                var predicate = PredicateBuilder.True<Person>();
                predicate = predicate.And(p => p.LastName.Equals("Grangeau"));
                predicate = predicate.And(p => p.BusinessEntityID == 20782);

                var person = personRepo.Single(predicate, p => p.BusinessEntity);
                var entId = person.BusinessEntity.BusinessEntityID;

                Assert.IsNotNull(person);
            }
        }

        [TestMethod]
        public void LastPersonTest()
        {
            using (var context = ContextAdapterFactory.CreateAdapter(new AdventureWorks2008R2Entities()))
            {
                var personRepo = new Repository<Person>(context);
                var predicate = PredicateBuilder.True<Person>();

                var person = personRepo.Last(predicate);
                Assert.IsNotNull(person);
            }
        }

        [TestMethod]
        public void FirstPersonTest()
        {
            using (var context = ContextAdapterFactory.CreateAdapter(new AdventureWorks2008R2Entities()))
            {
                var personRepo = new Repository<Person>(context);
                var predicate = PredicateBuilder.True<Person>();

                var person = personRepo.First(predicate);
                Assert.IsNotNull(person);
            }
        }

        [TestMethod]
        public void ReadAllPersonTest()
        {
            using (var context = ContextAdapterFactory.CreateAdapter(new AdventureWorks2008R2Entities()))
            {
                // Arrange
                var personRepo = new Repository<Person>(context);

                // Act
                var person = personRepo.ReadAll(p => p.BusinessEntity);

                // Assert
                Assert.IsTrue(person.Count() == 20021);
            }
        }

        [TestMethod]
        public void ReadPersonTest()
        {
            using (var context = ContextAdapterFactory.CreateAdapter(new AdventureWorks2008R2Entities()))
            {
                // Arrange
                var personRepo = new Repository<Person>(context);
                var predicate = PredicateBuilder.Create<Person>(p => p.LastName.Equals("Grangeau"));
                predicate = predicate.And(p => p.BusinessEntityID == 20782);

                // Act
                var persons = personRepo.Read(predicate, p => p.BusinessEntity);

                // Assert
                Assert.IsNotNull(persons);
                Assert.IsTrue(persons.Any());
                Assert.IsNotNull(persons.Select(t => t.BusinessEntity));
            }
        }

        [TestMethod]
        public void DeletePersonTest()
        {
            using (var context = ContextAdapterFactory.CreateAdapter(new AdventureWorks2008R2Entities()))
            {
                using (var unitOfWork = new UnitOfWork(context.CreateObjectContext()))
                {
                    var personRepo = new Repository<Person>(context);
                    var predicate = PredicateBuilder.True<Person>();

                    predicate = predicate.And(p => p.LastName.Equals("Grangeau"));
                    predicate = predicate.And(p => p.BusinessEntityID == 20780);

                    var person = personRepo.First(predicate, p => p.BusinessEntity);

                    personRepo.Delete(person);
                    unitOfWork.Commit();
                }
            }
        }
    }
}
