#region using

using System;
using System.Linq.Expressions;

#endregion

namespace EM.Framework.Data.Entity
{
    public class SimpleQuerySpecification<TEntity> : QueryableSpecification<TEntity> where TEntity : class
    {
        public SimpleQuerySpecification()
        {
        }

        public SimpleQuerySpecification(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public SimpleQuerySpecification<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    FetchStrategy.Include(include);
                }
            }
            ResetPredicate();
            return this;
        }
    }
}