
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using ImageSharp.Tests.Platform;
using Xunit;

namespace Svg.UnitTests
{

    /// <summary>
    /// Test Class of the feature to use Private Fonts in SVGs.
    /// Based on Issue 204.
    /// </summary>
    /// <remarks>
    /// Test use the following embedded resources:
    ///   - Issue204_PrivateFont\Text.svg
    ///   - Issue204_PrivateFont\BrushScriptMT2.ttf
    /// </remarks>
    public class PrivateFontsTests : SvgTestHelper
    {

        public static TheoryData<string, string, int> ExpectedRenderedFileSizes = new TheoryData<string, string, int>
        {
            { "Issue204_PrivateFont\\Text.svg", "Issue204_PrivateFont\\BrushScriptMT2.ttf", 3200 },
        };

        [Theory]
        [MemberData(nameof(ExpectedRenderedFileSizes))]
        public void TestImageIsRendered(string svgPath, string fontPath, int fileSize)
        {
            svgPath = FixPath(svgPath);
            fontPath = FixPath(fontPath);
            AddFontFromResource(SvgElement.PrivateFonts, fontPath);

            var svgDoc = SvgDocument.Open(svgPath);
            var img = SvgSharp.SystemDrawing.SvgDocumentExtensions.Render(svgDoc).AsImage();

            using (var ms = new MemoryStream())
            {
                img.Save(ms);

                Assert.True(ms.Length >= ExpectedSize, "Svg file does not match expected minimum size.");
            }
        }

        private void AddFontFromResource(IEnumerable<IFont> privateFontCollection, string fullFontResourceString)
        {
            throw new NotImplementedException();

            //var fontBytes = GetResourceBytes(fullFontResourceString);
            //var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            //Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            //privateFontCollection.AddMemoryFont(fontData, fontBytes.Length); // Add font to collection.
            //Marshal.FreeCoTaskMem(fontData); // Do not forget to frees the memory block.
        }
    }
}
