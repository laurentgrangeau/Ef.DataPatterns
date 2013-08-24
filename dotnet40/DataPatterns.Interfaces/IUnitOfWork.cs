namespace DataPatterns.Interfaces
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
