namespace DataPatterns.Interfaces
{
    using System;
    using System.Data;
    using System.Data.Objects;

    public interface IObjectSetFactory : IDisposable
    {
        ObjectContext CreateObjectContext();
        IObjectSet<T> CreateObjectSet<T>() where T : class;
        void ChangeObjectState(object entity, EntityState state);
    }
}
