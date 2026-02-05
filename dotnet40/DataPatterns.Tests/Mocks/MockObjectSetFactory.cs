using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using DataPatterns.Interfaces;

namespace DataPatterns.Tests.Mocks
{
    public class MockObjectSetFactory : IObjectSetFactory
    {
        private readonly Dictionary<Type, object> _objectSets = new Dictionary<Type, object>();

        public void RegisterObjectSet<T>(IObjectSet<T> objectSet) where T : class
        {
            _objectSets[typeof(T)] = objectSet;
        }

        public ObjectContext CreateObjectContext()
        {
            // Returning null as ObjectContext is hard to mock manually
            return null;
        }

        public IObjectSet<T> CreateObjectSet<T>() where T : class
        {
            return (IObjectSet<T>)_objectSets[typeof(T)];
        }

        public void ChangeObjectState(object entity, EntityState state)
        {
            // Do nothing
        }

        public void Dispose()
        {
            // Do nothing
        }
    }
}
