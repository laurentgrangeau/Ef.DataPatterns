namespace DataPatterns.Socle
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Objects;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<T> : ICrud<T>, IRepository<T>
        where T : class
    {
        private readonly IObjectSet<T> objectSet;
        private readonly IObjectSetFactory objectSetFactory;

        public Repository(IObjectSetFactory objectSetFactory)
        {
            this.objectSetFactory = objectSetFactory;
            objectSet = objectSetFactory.CreateObjectSet<T>();
        }

        public virtual void Create(T entity)
        {
            objectSet.AddObject(entity);
        }

        public virtual IQueryable<T> Read(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.Where(predicate);
        }

        public virtual void Update(T entity)
        {
            ObjectStateEntry stateEntry;

            if (!objectSetFactory.CreateObjectContext().ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry))
            {
                objectSetFactory.CreateObjectContext().AttachTo(typeof(T).Name, entity);
                objectSetFactory.ChangeObjectState(entity, EntityState.Modified);
            }
            else
            {
                objectSetFactory.CreateObjectContext().ApplyCurrentValues(typeof (T).Name, entity);
            }
        }

        public virtual void Delete(T entity)
        {
            objectSet.DeleteObject(entity);
        }

        public IQueryable<T> ReadAll(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query;
        }

        public virtual T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.Single(predicate);
        }

        public virtual T First(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.First(predicate);
        }

        public virtual T Last(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.Last(predicate);
        }

        public IQueryable<T> AsQueryable()
        {
            return objectSet;
        }

        protected IQueryable<T> AsQueryableWithIncludes(IEnumerable<Expression<Func<T, object>>> includeProperties)
        {
            var query = AsQueryable();

            return Includes(query, includeProperties);
        }

        protected IQueryable<T> Includes(IQueryable<T> query, IEnumerable<Expression<Func<T, object>>> includeProperties)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
