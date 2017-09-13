using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Composition;
using Xunit;

namespace CompositionTest
{
        public class BasicCompositionTest
        {
            [Fact]
            public void PointFreeComposition()
            {
                var logic = J.Composite<int, int, int, double>
                                            (Business.Pow
                                            , Business.Double
                                            , Business.Inc);
                Assert.Equal(expected: 64, actual: logic(3));
            }

            [Fact]
            public void IdentityComposition(){
                var result= Identity<int>.Of(3)
                                         .Map(Business.Inc)
                                         .Map(Business.Double)
                                         .Map(Business.Pow);
                Assert.Equal(expected:64,actual:result.Value);
            }
            [Fact]
            public void IdentityCompositionIsAStupidThing(){
                var result = new List<int>(){3}
                                         .Select(Business.Inc)
                                         .Select(Business.Double)
                                         .Select(Business.Pow);
                Assert.Equal(expected:64,actual:result.First());
                //or
                var a = Business.Inc(3);
                var b = Business.Double(a);
                var c = Business.Pow(b);
                Assert.Equal(expected:64,actual:c);
            }
        }
    }

