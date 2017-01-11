using Svg.Exceptions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace Svg.UnitTests
{
    
    public class MultiThreadingTest : SvgTestHelper
    {

        protected override string TestFile { get { return @"d:\temp\test.svg"; } }
        protected override int ExpectedSize { get { return 600000; } }

        private void LoadFile()
        {
            LoadSvg(TestFile);
        }

        
        [Fact]
        public void TestSingleThread()
        {
            LoadFile();
        }


        [Fact]
        public void TestMultiThread()
        {
            Parallel.For(0, 10, (x) =>
            {
                LoadFile();
            });
        }


        [Fact]
        public void SVGGivesMemoryExceptionOnTooManyParallelTest()
        {
            Exception exception = null;
            try
            {
                Parallel.For(0, 50, (x) =>
                {
                    LoadFile();
                });
            }
            catch (AggregateException ex)
            {
                exception = ex.InnerException;
            }

            Assert.NotNull(exception);
            Assert.IsType<SvgMemoryException>(exception);
        }
    }
}
