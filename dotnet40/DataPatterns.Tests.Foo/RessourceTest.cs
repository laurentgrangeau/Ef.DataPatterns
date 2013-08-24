using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DataPatterns.EntityFramework.Foo;
using DataPatterns.Interfaces;
using DataPatterns.Socle;
using DataPatterns.Socle.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataPatterns.Tests.Foo
{
    /// <summary>
    /// Description résumée pour RessourceTest
    /// </summary>
    [TestClass]
    public class RessourceTest
    {
        public RessourceTest()
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
        public void ReadRessourceTest()
        {
            using (var entities = new FOOEntities())
            {
                using (var context = ContextAdapterFactory.CreateAdapter(entities))
                {
                    var repo = new Repository<RessourceInt>(context);
                    var ressource = repo.ReadAll(BuildMereFilleInclusionsInt2().ToArray()).AsQueryable();

                    foreach (var item in ressource)
                    {
                        LoadIntObject(context, item);
                    }
                }
            }
        }

        private static List<Expression<Func<Ressource, object>>> BuildSearchInclusionsInt()
        {
            var lstInclude = BuildMereFilleInclusionsInt();
            lstInclude.Add(p => p.DemandeImplementation);
            return lstInclude;
        }

        private static void LoadIntObject(DbContext entities, RessourceInt item)
        {
            entities.Entry(item).Reference(x => x.Destination).Load();
            entities.Entry(item.Destination).Reference(x => x.RefCategorieDestination).Load();
            entities.Entry(item.Destination).Reference(x => x.RefPays).Load();
        }

        private static void LoadIntObject(IObjectSetFactory context, RessourceInt item)
        {
            context.CreateObjectContext().LoadProperty(item, i => i.Destination);
            context.CreateObjectContext().LoadProperty(item.Destination, d => d.RefCategorieDestination);
            context.CreateObjectContext().LoadProperty(item.Destination, d => d.RefPays);
        }

        private static List<Expression<Func<Ressource, object>>> BuildMereFilleInclusionsInt()
        {
            var lstInclude = new List<Expression<Func<Ressource, object>>> 
                                 { 
                                   p => p.RefPlanNumerotation, 
                                   p => p.RefEtatRessource, 
                                 };
            return lstInclude;
        }

        private static List<Expression<Func<RessourceInt, object>>> BuildMereFilleInclusionsInt2()
        {
            var lstInclude = new List<Expression<Func<RessourceInt, object>>> 
                                 { 
                                   p => p.RefPlanNumerotation, 
                                   p => p.RefEtatRessource,
                                   p => p.Destination
                                 };
            return lstInclude;
        }
    }
}
