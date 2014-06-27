using System;
using System.Linq;
using DataPatterns.EntityFramework.Foo;
using DataPatterns.Interfaces;
using DataPatterns.Socle;

namespace DataPatterns.Tests.Foo
{
    public class RessourceIntRepo : Repository<RessourceInt>
    {
        public RessourceIntRepo(IObjectSetFactory objectSetFactory) : base(objectSetFactory)
        {
        }

        public override IQueryable<RessourceInt> Read(System.Linq.Expressions.Expression<Func<RessourceInt, bool>> predicate, params System.Linq.Expressions.Expression<Func<RessourceInt, object>>[] includeProperties)
        {
            return base.Read(predicate, includeProperties);
        }
    }
}
