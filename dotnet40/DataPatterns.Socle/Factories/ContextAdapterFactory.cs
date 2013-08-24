namespace DataPatterns.Socle.Factories
{
    using Interfaces;
    using Adapters;
    using System.Data.Entity;
    using System.Data.Objects;

    public class ContextAdapterFactory
    {
        public static IObjectSetFactory CreateAdapter(ObjectContext context)
        {
            return new ObjectContextAdapter(context);
        }

        public static IObjectSetFactory CreateAdapter(DbContext context)
        {
            return new DbContextAdapter(context);
        }
    }
}
