namespace DataPatterns.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Simple interface for CRUD operations (Create, Read, Update, Delete)
    /// </summary>
    /// <typeparam name="T">The entity to implements CRUD operations</typeparam>
    public interface ICrud<T>
    {
        void Create(T entity);
        IEnumerable<T> Read(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Update(T entity);
        void Delete(T entity);
    }
}
