using System;

namespace Composition
{
    public class J
    {
        public static Func<T1, Tresult> Composite<T1, T2, Tresult>(Func<T2, Tresult> f, Func<T1, T2> g)
        {
            return (x) => f(g(x));
        }
        public static Func<T1, Tresult> Composite<T1, T2, T3, Tresult>(Func<T3, Tresult> f, Func<T2, T3> g, Func<T1, T2> h)
        {
            return (x) => f(g(h(x)));

        }
    }
}
