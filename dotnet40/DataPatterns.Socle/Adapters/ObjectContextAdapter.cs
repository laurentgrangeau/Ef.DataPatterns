namespace DataPatterns.Socle.Adapters
{
    using Interfaces;
    using System.Data;
    using System.Data.Objects;

    public class ObjectContextAdapter : IObjectSetFactory, IObjectContext
    {
        private readonly ObjectContext objectContext;

        public ObjectContextAdapter(ObjectContext objectContext)
        {
            this.objectContext = objectContext;
        }

        public ObjectContext CreateObjectContext()
        {
            return objectContext;
        }

        public void Dispose()
        {
            objectContext.Dispose();
        }

        public void SaveChanges()
        {
            objectContext.SaveChanges();
        }

        public IObjectSet<T> CreateObjectSet<T>() where T : class
        {
            return objectContext.CreateObjectSet<T>();
        }

        public void ChangeObjectState(object entity, EntityState state)
        {
            objectContext.ObjectStateManager.ChangeObjectState(entity, state);
        }
    }
}
