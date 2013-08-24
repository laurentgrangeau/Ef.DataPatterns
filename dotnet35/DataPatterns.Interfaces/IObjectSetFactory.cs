namespace DataPatterns.Interfaces
{
    using System;
    using System.Data.Objects;

    /// <summary>
    /// Interface for creating object context
    /// </summary>
    public interface IObjectSetFactory : IDisposable
    {
        ObjectContext CreateObjectContext();
    }
}
