#region using

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#endregion

namespace EM.Framework.Data.Repository
{
    public interface IFetchStrategy<T>
    {
        IEnumerable<Expression<Func<T, object>>> IncludePaths { get; }

        IFetchStrategy<T> Include(Expression<Func<T, object>> path);
    }
}