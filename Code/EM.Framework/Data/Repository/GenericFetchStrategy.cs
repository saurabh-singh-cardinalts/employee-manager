#region using

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#endregion

namespace EM.Framework.Data.Repository
{
    /// <summary>
    ///     Fetch Strategy
    ///     http://blog.willbeattie.net/2011/02/specification-pattern-entity-framework.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericFetchStrategy<T> : IFetchStrategy<T>
    {
        private readonly IList<Expression<Func<T, object>>> _properties;

        public GenericFetchStrategy()
        {
            _properties = new List<Expression<Func<T, object>>>();
        }

        #region IFetchStrategy<T> Members

        public IEnumerable<Expression<Func<T, object>>> IncludePaths
        {
            get { return _properties; }
        }

        public IFetchStrategy<T> Include(Expression<Func<T, object>> path)
        {
            _properties.Add(path);
            return this;
        }

        #endregion
    }
}