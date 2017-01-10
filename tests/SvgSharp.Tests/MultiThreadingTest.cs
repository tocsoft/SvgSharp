//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Svg.Exceptions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace Svg.UnitTests
{

    //[TestClass]
    public class MultiThreadingTest : SvgTestHelper
    {

        protected override string TestFile { get { return @"d:\temp\test.svg"; } }
        protected override int ExpectedSize { get { return 600000; } }

        private void LoadFile()
        {
            LoadSvg(GetXMLDocFromFile());
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
            Trace.WriteLine("Done");
        }


        [Fact]
        public void SVGGivesMemoryExceptionOnTooManyParallelTest()
        {
            Assert.Throws<SvgMemoryException>(() =>
            {
                try
                {
                    Parallel.For(0, 50, (x) =>
                    {
                        LoadFile();
                    });
                }
                catch (AggregateException ex)
                {
                    throw ex.InnerException;
                }
            });
        }
    }
}
