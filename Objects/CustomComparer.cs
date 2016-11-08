using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBO.Data.Objects
{
    public class CustomComparer<T> : Comparer<T>
    {
        private Func<T, IComparable> _getItem;
        public CustomComparer(Func<T, IComparable> getItem)
            : base()
        {
            _getItem = getItem;
        }

        public override int Compare(T x, T y)
        {
            return _getItem(x).CompareTo(_getItem(y));
        }
    }
}
