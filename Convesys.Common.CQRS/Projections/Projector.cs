using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Convesys.Kernel.Data;
using Convesys.Kernel.Data.ORM;
using Convesys.Kernel.Projections;

namespace Convesys.Common.CQRS.Projections
{
    public class Projector<TModel> : IProjector<TModel> where TModel : BaseModel
    {
        protected readonly IDbContext Context;

        public Projector(IDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public virtual IQueryable<TModel> GetAll()
        {
            return Context.Set<TModel>();
        }

        public IQueryable<TModel> Get(Guid id)
        {
            return this.Get(x => x.Id == id);
        }

        public IQueryable<TModel> Get(Expression<Func<TModel, bool>> expression)
        {
            return this.GetAll().Where(expression);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Context?.Dispose();
        }
    }
}