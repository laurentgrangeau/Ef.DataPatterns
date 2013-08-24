using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataPatterns.Socle;
using DataPatterns.EntityFramework;
using DataPatterns.Socle.Builders;

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

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
            using (var context = new ObjectContextAdapter(new AdventureWorks2008R2Entities()))
            {
                using (var unitOfWork = new UnitOfWork(context))
                {
                    var dateNow = DateTime.Now;

                    var personRepo = new Repository<Person>(context);
                    var businessEntityRepo = new Repository<BusinessEntity>(context);

                    var businessEntity = new BusinessEntity()
                    {
                        rowguid = Guid.NewGuid(),
                        ModifiedDate = dateNow
                    };

                    var person = new Person()
                    {
                        BusinessEntity = businessEntity,
                        FirstName = "Laurent",
                        LastName = "Grangeau",
                        PersonType = "GC",
                        rowguid = Guid.NewGuid(),
                        ModifiedDate = dateNow
                    };

                    businessEntityRepo.Create(businessEntity);
                    personRepo.Create(person);

                    unitOfWork.Commit();

                    Assert.IsNotNull(businessEntity.BusinessEntityID);
                    Assert.IsNotNull(person.BusinessEntityID);
                    Assert.IsNotNull(person.BusinessEntity);
                    Assert.IsTrue(person.FirstName.Equals("Laurent"));
                    Assert.IsTrue(person.LastName.Equals("Grangeau"));
                    Assert.IsTrue(person.PersonType.Equals("GC"));
                    Assert.IsTrue(person.ModifiedDate.Equals(dateNow));
                    Assert.IsTrue(businessEntity.ModifiedDate.Equals(dateNow));
                }
            }
        }

        [TestMethod]
        public void UpdatePersonTest()
        {
            using (var context = new ObjectContextAdapter(new AdventureWorks2008R2Entities()))
            {
                using (var unitOfWork = new UnitOfWork(context))
                {
                    var dateNow = DateTime.Now;

                    var personRepo = new Repository<Person>(context);

                    var predicate = PredicateBuilder.True<Person>();
                    predicate = predicate.And(p => p.LastName.Equals("Grangeau"));
                    predicate = predicate.And(p => p.BusinessEntityID == 20780);

                    var person = personRepo.Single(predicate);

                    person.ModifiedDate = dateNow;

                    personRepo.Update(person);
                    unitOfWork.Commit();

                    Assert.IsNull(person.BusinessEntity);
                    Assert.IsTrue(person.BusinessEntityID == 20780);
                    Assert.IsTrue(person.FirstName.Equals("Laurent"));
                    Assert.IsTrue(person.LastName.Equals("Grangeau"));
                    Assert.IsTrue(person.PersonType.Equals("GC"));
                    Assert.IsTrue(person.ModifiedDate.Equals(dateNow));
                }
            }
        }

        [TestMethod]
        public void SinglePersonTest()
        {
            using (var context = new ObjectContextAdapter(new AdventureWorks2008R2Entities()))
            {
                var personRepo = new Repository<Person>(context);

                var predicate = PredicateBuilder.True<Person>();
                predicate = predicate.And(p => p.LastName.Equals("Grangeau"));
                predicate = predicate.And(p => p.BusinessEntityID == 20780);

                var person = personRepo.Single(predicate, p => p.BusinessEntity);

                Assert.IsNotNull(person.BusinessEntity);
                Assert.IsTrue(person.BusinessEntityID == 20780);
                Assert.IsTrue(person.FirstName.Equals("Laurent"));
                Assert.IsTrue(person.LastName.Equals("Grangeau"));
                Assert.IsTrue(person.PersonType.Equals("GC"));
            }
        }

        [TestMethod]
        public void LastPersonTest()
        {
            using (var context = new ObjectContextAdapter(new AdventureWorks2008R2Entities()))
            {
                var personRepo = new Repository<Person>(context);
                var predicate = PredicateBuilder.True<Person>();

                var person = personRepo.Last(predicate);
            }
        }

        [TestMethod]
        public void FirstPersonTest()
        {
            using (var context = new ObjectContextAdapter(new AdventureWorks2008R2Entities()))
            {
                var personRepo = new Repository<Person>(context);
                var predicate = PredicateBuilder.True<Person>();

                var person = personRepo.First(predicate);
            }
        }
    }
}
