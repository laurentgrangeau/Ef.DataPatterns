namespace DataPatterns.Socle
{
    using DataPatterns.Interfaces;
    using System.Data.Objects;
   
    /// <summary>
    /// Adapter pattern for object context
    /// </summary>
    public class ObjectContextAdapter : IObjectSetFactory, IObjectContext
    {
        private readonly ObjectContext objectContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectContext"></param>
        public ObjectContextAdapter(ObjectContext objectContext)
        {
            this.objectContext = objectContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ObjectContext CreateObjectContext()
        {
            return this.objectContext;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.objectContext.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveChanges()
        {
            this.objectContext.SaveChanges();
        }
    }
}
