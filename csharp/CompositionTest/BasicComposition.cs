using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Composition;
using Xunit;

namespace CompositionTest
{
        public class Business
        {
            public static int Inc(int x) => x + 1;
            public static int Double(int x) => x * 2;
            public static int Pow(int x) => x * x;
        }

        public class BasicComposition
        {
            [Fact]
            public void PointFreeComposition()
            {
                var logic = J.Composite<int, int, int, int>(Business.Inc
                                                        , Business.Double
                                                        , Business.Pow);
                Assert.Equal(expected: 19, actual: logic(3));
            }

            [Fact]
            public void IdentityComposition(){
                var result= Identity<int>.Of(3)
                                         .Map(Business.Pow)
                                         .Map(Business.Double)
                                         .Map(Business.Inc);
                Assert.Equal(expected:19,actual:result.Value);
            }
            [Fact]
            public void IdentityCompositionIsAStupidThing(){
                var result = new List<int>(){3}
                                         .Select(Business.Pow)
                                         .Select(Business.Double)
                                         .Select(Business.Inc);
                Assert.Equal(expected:19,actual:result.First());
            }
        }
    }

