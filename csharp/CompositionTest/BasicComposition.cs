using System;
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
            public void PointFreeComposite()
            {
                var logic = J.Composite<int, int, int, int>(Business.Inc
                                                        , Business.Double
                                                        , Business.Pow);
                Assert.Equal(expected: 19, actual: logic(3));
            }
        }
    }

