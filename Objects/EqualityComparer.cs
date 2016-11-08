using System;
using System.Collections.Generic;

namespace DBO.Data.Objects
{
    public class CustomEqualityComparer<T> : IEqualityComparer<T>
    {
        private Func<T, T, bool> _compare;
        public CustomEqualityComparer(Func<T, T, bool> compare)
        {
            _compare = compare;
        }
        public bool Equals(T x, T y)
        {
            return _compare(x, y);
        }

        public int GetHashCode(T obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
