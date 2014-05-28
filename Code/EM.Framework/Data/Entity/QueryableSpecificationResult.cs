#region using

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EM.Framework.Data.Repository;

#endregion

namespace EM.Framework.Data.Entity
{
    /// <summary>
    ///     Specification result class contains common functionality for filtering result.
    /// </summary>
    /// <typeparam name="TEntity">Domain entity.</typeparam>
    public class QueryableSpecificationResult<TEntity> : ISpecificationResult<TEntity>
        where TEntity : class
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public QueryableSpecificationResult(IQueryable<TEntity> queryable)
        {
            Queryable = queryable;
        }

        /// <summary>
        ///     Gets or sets IQueryable interface for domain entity.
        /// </summary>
        public IQueryable<TEntity> Queryable { get; private set; }

        /// <summary>
        ///     Takes given count of the records represented by the specification.
        /// </summary>
        public ISpecificationResult<TEntity> Take(int count)
        {
            Queryable = Queryable.Take(count);
            return this;
        }

        /// <summary>
        ///     Bypasses a specified number of elements in a sequence and then returns
        ///     the remaining elements.
        /// </summary>
        public ISpecificationResult<TEntity> Skip(int count)
        {
            Queryable = Queryable.Skip(count);
            return this;
        }

        /// <summary>
        ///     Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        public ISpecificationResult<TEntity> OrderByAscending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            Queryable = Queryable.OrderBy(keySelector);
            return this;
        }

        /// <summary>
        ///     Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        public ISpecificationResult<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            Queryable = Queryable.OrderByDescending(keySelector);
            return this;
        }


        public ISpecificationResult<TEntity> AsNoTracking()
        {
            Queryable = Queryable.AsNoTracking();
            return this;
        }

        /// <summary>
        ///     Executes the specification and query behind it and returns list of records
        ///     that matches criteria.
        /// </summary>
        public List<TEntity> ToList()
        {
            return Queryable.ToList();
        }


        /// <summary>
        ///     Executes the specification and query behind it and returns the only record
        ///     of a sequence. Throws if there is not exactly one element in the sequence.
        /// </summary>
        public TEntity Single()
        {
            return Queryable.Single();
        }

        /// <summary>
        ///     Returns the only element of a sequence, or a default value if the sequence is empty;
        ///     this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        public TEntity SingleOrDefault()
        {
            return Queryable.SingleOrDefault();
        }

        public int Count()
        {
            return Queryable.Count();
        }

        public bool Exists()
        {
            return Queryable.Count() != 0;
        }

        public IList<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> keySelector)
        {
            var x = Queryable.Select(keySelector).ToList();
            return x;
        }

        public Dictionary<TKey, TEntity> ToDictionary<TKey>(Func<TEntity, TKey> keySelector)
        {
            return Queryable.ToDictionary(keySelector);
        }
    }
}