namespace DataPatterns.Socle
{
    using DataPatterns.Interfaces;

    /// <summary>
    /// Implementation of Unit Of Work pattern
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IObjectContext objectContext;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="objectContext">An IObjectContext context</param>
        public UnitOfWork(IObjectContext objectContext)
        {
            this.objectContext = objectContext;
        }

        /// <summary>
        /// Save all changes to the current context
        /// </summary>
        public void Commit()
        {
            this.objectContext.SaveChanges();
        }

        /// <summary>
        /// Dispose the current context
        /// </summary>
        public void Dispose()
        {
            this.objectContext.Dispose();
        }
    }
}
