namespace DataPatterns.Socle
{
    using Interfaces;
    using System.Data.Objects;

    /// <summary>
    /// Implementation of Unit Of Work pattern
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IObjectContext _objectContext;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="objectContext">An IObjectContext context</param>
        public UnitOfWork(IObjectContext objectContext)
        {
            _objectContext = objectContext;
        }

        /// <summary>
        /// Save all changes to the current context
        /// </summary>
        public void Commit()
        {
            _objectContext.SaveChanges();
        }

        /// <summary>
        /// Dispose the current context
        /// </summary>
        public void Dispose()
        {
            _objectContext.Dispose();
        }
    }
}
