﻿// ***********************************************************************
// Copyright (c) 2009 Charlie Poole, Rob Prouse
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections;
using System.Reflection;

namespace TestBase
{
    /// <summary>
    ///     NUnitComparer encapsulates NUnit's default behavior
    ///     in comparing two objects.
    /// </summary>
    public class NUnitComparer : IComparer
    {
        /// <summary>
        ///     Returns the default NUnitComparer.
        /// </summary>
        public static NUnitComparer Default => new NUnitComparer();

        /// <summary>
        ///     Compares two objects
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            if (x      == null) return y == null ? 0 : -1;
            else if (y == null) return +1;

            if (x is char c && y is char) return c == (char) y ? 0 : 1;

            if (Numerics.IsNumericType(x) && Numerics.IsNumericType(y))
                return Numerics.Compare(x, y);

            if (x is IComparable comparable) return comparable.CompareTo(y);

            if (y is IComparable comparable1) return -comparable1.CompareTo(x);

            var xType = x.GetType();
            var yType = y.GetType();

            var method = xType.GetTypeInfo().GetMethod("CompareTo", new[] {yType});
            if (method != null) return (int) method.Invoke(x, new[] {y});

            method = yType.GetTypeInfo().GetMethod("CompareTo", new[] {xType});
            if (method != null) return -(int) method.Invoke(y, new[] {x});

            throw new ArgumentException("Neither value implements IComparable or IComparable<T>");
        }
    }
}
