using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Composition;
using Xunit;
using B = Composition.Business;

namespace CompositionTest
{
    public class IdentityCompositionTest
    {
        [Fact]
        public void IdentityFunctor()
        {
            var result = Identity<int>.Of(3)
                                     .Map(B.Inc)
                                     .Map(B.Double)
                                     .Map(B.Pow);
            Assert.Equal(expected: 64, actual: result.Value);
        }

        [Fact]
        public void AsociativeLawFunctor()
        {
            //F.map(x => f(g(x))) is equivalent to F.map(g).map(f)
            var result=Identity<int>.Of(3)
                .Map(J.Composite<int, int, int, int>(B.Pow, B.Double, B.Inc));
            Assert.Equal(expected: 64, actual: result.Value);

        }
             [Fact]
        public void IdentityLawFunctor()
        {
            //F.map(a => a) is equivalent to F (identity)
            var result1=Identity<int>.Of(3);
            var result2 = Identity<int>.Of(3).Map(a=>a);
            Assert.Equal(expected: new int [3,3], actual: new int [result1.Value, result2.Value]);
        }

        [Fact]
        public void IdentityCompositionIsAStupidThing()
        {
            var result = new List<int>() { 3 }
                                     .Select(B.Inc)
                                     .Select(B.Double)
                                     .Select(B.Pow);
            Assert.Equal(expected: 64, actual: result.First());
            //or
            var a = B.Inc(3);
            var b = B.Double(a);
            var c = B.Pow(b);
            Assert.Equal(expected: 64, actual: c);
            Assert.Equal(expected: 64, actual: B.Pow(B.Double(B.Inc(3))));
        }
        [Fact]
        public void MathInCsharpIsTriky()
        {
            var a = B.Inc(-3);
            var b = Math.Sqrt(a);
            Assert.Equal(expected: double.NaN, actual: b);
            var c = B.Pow(b);
            Assert.Equal(expected: double.NaN, actual: b);
        }

        [Fact]
        public void MathInCsharpIsTrikyBut()
        {
            var a = Math.Sqrt(-4);
            var b = B.Pow(a);
            Assert.Throws<OverflowException>(() => Convert.ToInt32(b));
        }

        [Fact]
        public void IdentityNotSolveTheProblem()
        {
            Assert.Throws<OverflowException>(() =>
                    Identity<double>.Of(-4)
                        .Map(Math.Sqrt)
                        .Map(Convert.ToInt32));
        }
    }
}

