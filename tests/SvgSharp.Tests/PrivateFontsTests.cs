
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        private const string PrivateFontSvg = "Issue204_PrivateFont\\Text.svg";
        private const string PrivateFont = "Issue204_PrivateFont\\BrushScriptMT2.ttf";
        //private const string PrivateFontName = "Brush Script MT2";

        protected override int ExpectedSize { get { return 3200; } } //3512


        [Fact]
        public void TestPrivateFont()
        {
            AddFontFromResource(SvgElement.PrivateFonts, GetFullResourceString(PrivateFont));
            LoadSvg(PrivateFontSvg);
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
