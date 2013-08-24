namespace DataPatterns.Socle
{
    using DataPatterns.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Implementation of the Repository pattern for EF, along with CRUD operations
    /// </summary>
    /// <typeparam name="T">The entity to perform repository and CRUD operations</typeparam>
    public class Repository<T> : ICrud<T>, IRepository<T> where T : IEntityWithKey
    {
        private readonly ObjectContext objectContext;
        private readonly IObjectSetFactory objectSetFactory;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="objectSetFactory"></param>
        public Repository(IObjectSetFactory objectSetFactory)
        {
            this.objectSetFactory = objectSetFactory;
            this.objectContext = objectSetFactory.CreateObjectContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Create(T entity)
        {
            this.objectContext.AddObject(typeof(T).Name, entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Read(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.Where(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            this.objectContext.ApplyPropertyChanges(entity.EntityKey.EntitySetName, entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            this.objectContext.DeleteObject(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<T> ReadAll(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.ToList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.Single<T>(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.SingleOrDefault<T>(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual T First(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.First<T>(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.FirstOrDefault<T>(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual T Last(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = AsQueryableWithIncludes(includeProperties);

            return query.Last<T>(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> AsQueryable()
        {
            return this.objectContext.CreateQuery<T>(string.Format("[{0}]", typeof(T).Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        protected IQueryable<T> AsQueryableWithIncludes(IEnumerable<Expression<Func<T, object>>> includeProperties)
        {
            var query = AsQueryable();

            return Includes(query, includeProperties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        protected IQueryable<T> Includes(IQueryable<T> query, IEnumerable<Expression<Func<T, object>>> includeProperties)
        {
            foreach (var includeProperty in includeProperties)
            {
                int i = includeProperty.Body.ToString().IndexOf('.');
                string pathString = includeProperty.Body.ToString().Substring(i + 1);

                query = (query as ObjectQuery<T>).Include(pathString);
            }

            return query;
        }
    }
}
