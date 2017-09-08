using System;

namespace Composition
{
    public class Identity<T>
    {
        public Identity(T value) => Value = value;
        public static Identity<T> Of(T value) => new Identity<T>(value);

        public Identity<Tresult> Map<Tresult>(Func<T, Tresult> f)
        {
            return Identity<Tresult>.Of(f(Value));
        }

        public T Value { get; private set; }
    }
}