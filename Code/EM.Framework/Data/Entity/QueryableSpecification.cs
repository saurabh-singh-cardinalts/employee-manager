#region using

using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EM.Framework.Data.Repository;

#endregion

namespace EM.Framework.Data.Entity
{
    /// <summary>
    ///     Base class for <see cref="IQueryable" /> based specifications.
    /// </summary>
    public abstract class QueryableSpecification<T> : ISpecification<T>
        where T : class
    {
        protected GenericFetchStrategy<T> FetchStrategy { get; set; }

        protected Expression<Func<T, bool>> Predicate { get; set; }

        protected IUnitOfWork UnitOfWork { get; private set; }

        protected QueryableSpecification()
        {
        }

        protected QueryableSpecification(IUnitOfWork unitOfWork)
        {
            Initialize(unitOfWork);
        }

        public void Initialize(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ResetSpecification();
        }

        protected void ResetPredicate()
        {
            Predicate = T => true;
        }

        protected void ResetFetchStrategy()
        {
            FetchStrategy = new GenericFetchStrategy<T>();
        }

        protected void ResetSpecification()
        {
            ResetPredicate();
            ResetFetchStrategy();
        }

        protected IQueryable<T> Query
        {
            get
            {
                var uow = UnitOfWork as UnitOfWork;
                if (uow != null)
                {
                    var query = uow.Queryable<T>();
                    //first include              
                    query = FetchStrategy.IncludePaths.Aggregate(query, (current, path) => current.Include(path));
                    return query.Where(Predicate);
                }
                return null;
            }
        }

        public virtual ISpecificationResult<T> ToResult()
        {
            var result = new QueryableSpecificationResult<T>(Query);
            ResetSpecification();
            return result;
        }
    }
}