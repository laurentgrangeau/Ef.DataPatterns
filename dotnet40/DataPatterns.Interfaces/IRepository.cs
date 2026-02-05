namespace DataPatterns.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T>
    {
        IQueryable<T> ReadAll(params Expression<Func<T, object>>[] includeProperties);
        T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T SingleOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T First(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T Last(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> AsQueryable();
    }
}
