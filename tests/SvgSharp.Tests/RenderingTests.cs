
using System.IO;
using ImageSharp.Tests.Platform;
using Xunit;

namespace Svg.UnitTests
{
    public class RenderingTests : SvgTestHelper
    {
        public static TheoryData<string, int> ExpectedRenderedFileSizes = new TheoryData<string, int>
        {
            { "hotfix_image_data_uri\\Speedometer.svg", 160000 },
            { "Issue225_LargeUri\\Speedometer.svg", 160000 },
            { "Issue212_MakerEnd\\OperatingPlan.svg", 5000 }
        };

        [Theory]
        [MemberData(nameof(ExpectedRenderedFileSizes))]
        public void TestImageIsRendered(string path, int fileSize)
        {
            path = FixPath(path);

            var svgDoc = SvgDocument.Open(path);
            var img = SvgSharp.SystemDrawing.SvgDocumentExtensions.Render(svgDoc).AsImage();

            using (var ms = new MemoryStream())
            {
                img.Save(ms);

                Assert.True(ms.Length >= ExpectedSize, "Svg file does not match expected minimum size.");
            }

        }
    }
}