using System;

namespace Composition
{
    public interface IEither<A, B>
    {
        IEither<A, C> Map<C>(Func<B, C> f);
        void Fold(Action<A> f, Action<B> g);
        B OrDefault(B v);
    }

    public static class Either<A, B>
    {
        public static IEither<A, B> Right(B v) => new _Right(v);
        public static IEither<A, B> Of(B v) => new _Right(v);
        public static IEither<A, B> Left(A v) => new _Left(v);
         public static IEither<Exception,B> Try(Func<B> f)
        {
            try
            {
                return Either<Exception,B>.Right(f());
            }
            catch (Exception e)
            {
                
                return Either<Exception,B>.Left(e);
            }
        }


        private class _Right : IEither<A, B>
        {
            private B value;

            public _Right(B value)
            {
                this.value = value;
            }
            public static IEither<A, B> Of(B value) => new _Right(value);

            public IEither<A, C> Map<C>(Func<B, C> f)  => Either<A,C>.Right(f(this.value));
            public void Fold(Action<A> f, Action<B> g) => g(this.value);
            public B OrDefault(B v) => this.value;
        }

        private class _Left: IEither<A, B>
        {
            private A value;
            public _Left(A value) => this.value = value;
            public static IEither<A, B> Of(A value) => Either<A, B>.Left(value);

            public IEither<A, C> Map<C>(Func<B, C> f) => Either<A,C>.Left(this.value);
            public void Fold(Action<A> f, Action<B> g) => f(this.value);
            public B OrDefault(B v) => v;
        }

       
    }
}