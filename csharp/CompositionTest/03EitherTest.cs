using System;
using Composition;
using Xunit;
using B = Composition.Business;

namespace CompositionTest
{
    public class EitherTest
    {
        [Fact]
        public void RightIsLikeIdentity()
        {
            var result = Either<string, int>.Right(3)
                                     .Map(B.Inc)
                                     .Map(B.Double)
                                     .Map(B.Pow);

            result.Fold(null, x => Assert.Equal(expected: 64, actual: x));
            Assert.Equal(expected: 64, actual: result.OrDefault(0));
        }

        [Fact]
        public void ButLeftNotChangeNothing()
        {
            var result = Either<int, int>.Left(3)
                                     .Map(B.Inc)
                                     .Map(B.Double)
                                     .Map(B.Pow);

            result.Fold(x => Assert.Equal(expected: 3, actual: x), null);
            Assert.Equal(expected: 0, actual: result.OrDefault(0));
        }

        [Fact]
        public void TransformExceptionToEither()
        {
            var result = Either<Exception, int>
                            .Try(() => Convert.ToInt32(Math.Sqrt(-4)));
            Assert.Equal(expected: 0, actual: result.OrDefault(0));
            result.Fold(x => Assert.IsType(Type.GetType("System.OverflowException"), x), null);

        }

        [Fact]
        public void EitherCanManageProblemsButUgglyFuctor()
        {
            var result = Either<Exception, double>.Of(4)
            .Map(Math.Sqrt)
            .Map(J.SafeConvertToInt32);

            var actual = result.OrDefault(Either<Exception, int>.Of(0)).OrDefault(0);
            Assert.Equal(expected: 2, actual: actual);

            result.Fold(null, x => x.Fold(null, y => Assert.Equal(expected: 2, actual: y)));
        }

        [Fact]
        public void EitherCanManageProblemsButUgglyFuctoWithTheException()
        {
            var result = Either<Exception, double>.Of(-4)
            .Map(Math.Sqrt)
            .Map(J.SafeConvertToInt32);

            var actual = result.OrDefault(Either<Exception, int>.Of(0)).OrDefault(0);
            Assert.Equal(expected: 0, actual: actual);

            result.Fold(null, x => x.Fold(y => Assert.IsType(Type.GetType("System.OverflowException"), y), null));
        }

        [Fact]
        public void ChainToResque()
        {

            var result1 = Either<Exception, double>.Of(4)
                .Map(Math.Sqrt)
                .Chain(J.SafeConvertToInt32);
            Assert.Equal(expected: 2, actual: result1.OrDefault(0));

            var result2 = Either<Exception, double>.Of(-4)
                .Map(Math.Sqrt)
                .Chain(J.SafeConvertToInt32);
            Assert.Equal(expected: 0, actual: result2.OrDefault(0));
            result2.Fold(x => Assert.IsType(Type.GetType("System.OverflowException"), x), null);

        }
    }
}

