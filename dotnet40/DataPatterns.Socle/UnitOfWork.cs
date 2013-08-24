namespace DataPatterns.Socle
{
    using Interfaces;
    using System.Data.Objects;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ObjectContext objectContext;

        public UnitOfWork(ObjectContext objectContext)
        {
            this.objectContext = objectContext;
        }

        public void Commit()
        {
            objectContext.SaveChanges();
        }

        public void Dispose()
        {
            objectContext.Dispose();
        }
    }
}
