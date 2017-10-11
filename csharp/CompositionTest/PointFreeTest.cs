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
    public class PointFreeTest
    {
        [Fact]
        public void PointFreeComposition()
        {
            var logic = J.Composite<int, int, int, int>
                                        (B.Pow
                                        , B.Double
                                        , B.Inc);
            Assert.Equal(expected: 64, actual: logic(3));
        }

       
    }
}

