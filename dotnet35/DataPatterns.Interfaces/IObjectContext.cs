namespace DataPatterns.Interfaces
{
    using System;

    /// <summary>
    /// Interface for saving change to the current context
    /// </summary>
    public interface IObjectContext : IDisposable
    {
        void SaveChanges();
    }
}
