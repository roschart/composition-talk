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
            var result = Either<string,int>.Right(3)
                                     .Map(B.Inc)
                                     .Map(B.Double)
                                     .Map(B.Pow);
    
            result.Fold(null,x=>Assert.Equal(expected: 64,actual:x));
            Assert.Equal(expected: 64, actual: result.OrDefault(0));
        }

        [Fact]
         public void ButLeftNotChangeNothing()
        {
            var result = Either<int,int>.Left(3)
                                     .Map(B.Inc)
                                     .Map(B.Double)
                                     .Map(B.Pow);
    
            result.Fold(x=>Assert.Equal(expected: 3,actual:x),null);
            Assert.Equal(expected: 0, actual: result.OrDefault(0));
        }


        
    }
}

