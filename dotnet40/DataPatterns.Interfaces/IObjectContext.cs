namespace DataPatterns.Interfaces
{
    using System;

    public interface IObjectContext : IDisposable
    {
        void SaveChanges();
    }
}
