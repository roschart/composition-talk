using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Composition;
using Xunit;
using B=Composition.Business;

namespace CompositionTest
{
        public class BasicCompositionTest
        {
            [Fact]
            public void PointFreeComposition()
            {
                var logic = J.Composite<int, int, int, double>
                                            (B.Pow
                                            , B.Double
                                            , B.Inc);
                Assert.Equal(expected: 64, actual: logic(3));
            }

            [Fact]
            public void IdentityComposition(){
                var result= Identity<int>.Of(3)
                                         .Map(B.Inc)
                                         .Map(B.Double)
                                         .Map(B.Pow);
                Assert.Equal(expected:64,actual:result.Value);
            }
            [Fact]
            public void IdentityCompositionIsAStupidThing(){
                var result = new List<int>(){3}
                                         .Select(B.Inc)
                                         .Select(B.Double)
                                         .Select(B.Pow);
                Assert.Equal(expected:64,actual:result.First());
                //or
                var a = B.Inc(3);
                var b = B.Double(a);
                var c = B.Pow(b);
                Assert.Equal(expected:64,actual:c);
                Assert.Equal(expected:64, actual:B.Pow(B.Double(B.Inc(3))));
            }
            [Fact]
            public void WhatHappenWithErrors(){
                

            }


        }
    }

