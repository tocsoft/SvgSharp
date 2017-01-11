using Svg.DataTypes;
using System;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace Svg.UnitTests
{

    /// <summary>
    /// Test Class of rendering SVGs with a large embedded image
    /// Based on Issue 225
    /// </summary>
    /// <remarks>
    /// Test use the following embedded resources:
    ///   - Issue225_LargeUri\Speedometer.svg
    /// </remarks>
    
    public class LargeEmbeddedImageTest : SvgTestHelper
    {
        protected override string TestResource { get { return "Issue225_LargeUri\\Speedometer.svg"; } }
        protected override int ExpectedSize { get { return 160000; } } 

        [Fact]
        public void TestImageIsRendered()
        {
            LoadSvg(TestResource);
        }
    }
}
