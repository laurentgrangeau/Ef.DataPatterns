namespace DataPatterns.Socle.Extensions
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;

    public static class DbContextExtension
    {
        public static ObjectContext GetObjectContext(this DbContext dbContext)
        {
            return ((IObjectContextAdapter)dbContext).ObjectContext;
        }
    }
}
