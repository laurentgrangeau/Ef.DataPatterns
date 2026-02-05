using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace DataPatterns.Tests.Mocks
{
    public class MockObjectSet<T> : IObjectSet<T> where T : class
    {
        private readonly IList<T> _data;
        private readonly IQueryable<T> _query;

        public MockObjectSet(IList<T> data)
        {
            _data = data;
            _query = data.AsQueryable();
        }

        public void AddObject(T entity)
        {
            _data.Add(entity);
        }

        public void DeleteObject(T entity)
        {
            _data.Remove(entity);
        }

        public void Attach(T entity)
        {
            // Do nothing
        }

        public void Detach(T entity)
        {
            _data.Remove(entity);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _query.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _query.GetEnumerator();
        }

        public Expression Expression
        {
            get { return _query.Expression; }
        }

        public Type ElementType
        {
            get { return _query.ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return _query.Provider; }
        }
    }
}
