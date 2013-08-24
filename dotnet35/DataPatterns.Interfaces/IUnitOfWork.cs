namespace DataPatterns.Interfaces
{
    using System;

    /// <summary>
    /// Interface for Unit Of Work pattern
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
