#region using

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace EM.Framework.Extensions
{
    public static class EqualityComparer
    {
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> list, Func<T, object> keyExtractor)
        {
            return list.Distinct(new KeyEqualityComparer<T>(keyExtractor));
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> list, IEnumerable<T> second,
                                               Func<T, object> keyExtractor)
        {
            return list.Except(second, new KeyEqualityComparer<T>(keyExtractor));
        }
    }

    public class KeyEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object> _keyExtractor;

        public KeyEqualityComparer(Func<T, object> keyExtractor)
        {
            _keyExtractor = keyExtractor;
        }

        public bool Equals(T x, T y)
        {
            return _keyExtractor(x).Equals(_keyExtractor(y));
        }

        public int GetHashCode(T obj)
        {
            return _keyExtractor(obj).GetHashCode();
        }
    }
}