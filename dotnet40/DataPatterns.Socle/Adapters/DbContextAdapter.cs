namespace DataPatterns.Socle.Adapters
{
    using Extensions;
    using System.Data.Entity;

    public class DbContextAdapter : ObjectContextAdapter
    {
        public DbContextAdapter(DbContext dbContext)
            : base(dbContext.GetObjectContext())
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
        }
    }
}
