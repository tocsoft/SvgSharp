
using System.IO;
using System.Linq;
using System.Xml;
using Xunit;

namespace Svg.UnitTests
{
    /// <summary>
    /// Tests that the deep copy of a <see cref="SvgText"/> is done correctly where the
    /// text element has contains only text and now other elements like <see cref="SvgTextSpan"/>.
    /// </summary>
    /// <seealso cref="Svg.UnitTests.SvgTestHelper" />
    public class SvgTextElementDeepCopyTest : SvgTestHelper
    {
        private const string PureTextElementSvg = "Issue_TextElement\\Text.svg";
        
        [Fact]
        public void TestSvgTextElementDeepCopy()
        {
            var svgDocument = OpenSvg(PureTextElementSvg);
            CheckDocument(svgDocument);

            var deepCopy = (SvgDocument)svgDocument.DeepCopy<SvgDocument>();
            CheckDocument(deepCopy);
        }

        /// <summary>
        /// Checks the document if it contains the correct information when exported to XML.
        /// </summary>
        /// <param name="svgDocument">The SVG document to check.</param>
        private static void CheckDocument(SvgDocument svgDocument)
        {
            Assert.Equal(2, svgDocument.Children.Count);
            Assert.IsType< SvgDefinitionList>(svgDocument.Children[0]);
            Assert.IsType< SvgText>(svgDocument.Children[1]);

            var textElement = (SvgText)svgDocument.Children[1];
            Assert.Equal("IP", textElement.Content);

            var memoryStream = new MemoryStream();
            svgDocument.Write(memoryStream);

            memoryStream.Seek(0, SeekOrigin.Begin);

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(memoryStream);

            Assert.Equal(2, xmlDocument.ChildNodes.Count);
            var svgNode = xmlDocument.ChildNodes[1];

            // Filter all significant whitespaces.
            var svgChildren = svgNode.ChildNodes
                .OfType<XmlNode>()
                .Where(item => item.GetType() != typeof(XmlSignificantWhitespace))
                .OfType<XmlNode>()
                .ToArray();

            Assert.Equal(2, svgChildren.Length);
            var textNode = svgChildren[1];

            Assert.Equal("text", textNode.Name);
            Assert.Equal("IP", textNode.InnerText);
        }
    }
}