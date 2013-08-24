namespace DataPatterns.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Inteface for Repository pattern
    /// </summary>
    /// <typeparam name="T">The entity to query from the repository</typeparam>
    public interface IRepository<T>
    {
        IEnumerable<T> ReadAll(params Expression<Func<T, object>>[] includeProperties);
        T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T SingleOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T First(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T Last(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> AsQueryable();
    }
}
