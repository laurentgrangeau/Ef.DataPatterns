namespace DataPatterns.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface ICrud<T>
    {
        void Create(T entity);
        IQueryable<T> Read(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Update(T entity);
        void Delete(T entity);
    }
}
