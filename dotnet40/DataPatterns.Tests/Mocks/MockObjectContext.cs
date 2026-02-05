using System;
using DataPatterns.Interfaces;

namespace DataPatterns.Tests.Mocks
{
    public class MockObjectContext : IObjectContext
    {
        public bool SaveChangesCalled { get; private set; }
        public bool DisposeCalled { get; private set; }

        public void SaveChanges()
        {
            SaveChangesCalled = true;
        }

        public void Dispose()
        {
            DisposeCalled = true;
        }
    }
}
