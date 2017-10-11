using System;

namespace Composition
{
    public interface Either<A, B>
    {
        Either<A, C> Map<C>(Func<B, C> f);
        void Fold(Action<A> f, Action<B> g);
         B OrDefault(B v);
    }

    public class Right<A, B> : Either<A, B>
    {
        private B value;

        public Right(B value)
        {
            this.value = value;
        }
        public static Either<A, B> Of(B value) => new Right<A, B>(value);

        public Either<A, C> Map<C>(Func<B, C> f)
        {
            return Right<A, C>.Of(f(this.value));
        }
        public void Fold(Action<A> f, Action<B> g) => g(this.value);
        public B OrDefault(B v) => this.value;
    }

    public class Left<A, B> : Either<A, B>
    {
        private A value;
        public Left(A value) => this.value = value;
        public static Either<A, B> Of(A value) => new Left<A, B>(value);

        public Either<A, C> Map<C>(Func<B, C> f) => new Left<A, C>(this.value);
        public void Fold(Action<A> f, Action<B> g) => f(this.value);
        public B OrDefault(B v) => v;
    }

}