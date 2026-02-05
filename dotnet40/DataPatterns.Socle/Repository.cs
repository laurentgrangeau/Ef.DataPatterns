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

    /// <summary>
    /// Implementation of the Repository pattern for EF, along with CRUD operations
    /// </summary>
    /// <typeparam name="T">The entity to perform repository and CRUD operations</typeparam>
    public class Repository<T> : ICrud<T>, IRepository<T>
        where T : class
    {
        private readonly IObjectSet<T> _objectSet;
        private readonly IObjectSetFactory _objectSetFactory;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="objectSetFactory">The object set factory</param>
        public Repository(IObjectSetFactory objectSetFactory)
        {
            _objectSetFactory = objectSetFactory;
            _objectSet = objectSetFactory.CreateObjectSet<T>();
        }

        /// <summary>
        /// Create an entity
        /// </summary>
        /// <param name="entity">The entity to create</param>
        public virtual void Create(T entity)
        {
            _objectSet.AddObject(entity);
        }

        /// <summary>
        /// Read entities based on a predicate
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <param name="includeProperties">Properties to include</param>
        /// <returns>A queryable of entities</returns>
        public virtual IQueryable<T> Read(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.Where(predicate);
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity">The entity to update</param>
        public virtual void Update(T entity)
        {
            ObjectStateEntry stateEntry;
            var context = _objectSetFactory.CreateObjectContext();
            var entitySetName = typeof(T).Name;

            if (!context.ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry))
            {
                context.AttachTo(entitySetName, entity);
                _objectSetFactory.ChangeObjectState(entity, EntityState.Modified);
            }
            else
            {
                context.ApplyCurrentValues(entitySetName, entity);
            }
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public virtual void Delete(T entity)
        {
            _objectSet.DeleteObject(entity);
        }

        /// <summary>
        /// Read all entities
        /// </summary>
        /// <param name="includeProperties">Properties to include</param>
        /// <returns>A queryable of entities</returns>
        public IQueryable<T> ReadAll(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query;
        }

        /// <summary>
        /// Get a single entity
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <param name="includeProperties">Properties to include</param>
        /// <returns>The entity</returns>
        public virtual T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.Single(predicate);
        }

        /// <summary>
        /// Get a single entity or default
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <param name="includeProperties">Properties to include</param>
        /// <returns>The entity or default</returns>
        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Get the first entity
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <param name="includeProperties">Properties to include</param>
        /// <returns>The entity</returns>
        public virtual T First(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.First(predicate);
        }

        /// <summary>
        /// Get the first entity or default
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <param name="includeProperties">Properties to include</param>
        /// <returns>The entity or default</returns>
        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Get the last entity
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <param name="includeProperties">Properties to include</param>
        /// <returns>The entity</returns>
        public virtual T Last(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.Last(predicate);
        }

        /// <summary>
        /// Get as queryable
        /// </summary>
        /// <returns>A queryable of entities</returns>
        public IQueryable<T> AsQueryable()
        {
            return _objectSet;
        }

        /// <summary>
        /// Get as queryable with includes
        /// </summary>
        /// <param name="includeProperties">Properties to include</param>
        /// <returns>A queryable of entities</returns>
        protected IQueryable<T> AsQueryableWithIncludes(IEnumerable<Expression<Func<T, object>>> includeProperties)
        {
            var query = AsQueryable();

            return Includes(query, includeProperties);
        }

        /// <summary>
        /// Apply includes to a query
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="includeProperties">Properties to include</param>
        /// <returns>A query with includes</returns>
        protected IQueryable<T> Includes(IQueryable<T> query, IEnumerable<Expression<Func<T, object>>> includeProperties)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
