//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Xunit;

namespace Svg.UnitTests
{
    public class SvgTextTests
    {

        [Fact]
        public void TextPropertyAffectsSvgOutput()
        {
            //throw new NotImplementedException();
            var document = new SvgDocument();
            document.Children.Add(new SvgText { Text = "test1" });
            using (var stream = new MemoryStream())
            {
                document.Write(stream);
                stream.Position = 0;

                var xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);
                Assert.Equal("test1", xmlDoc.DocumentElement.FirstChild.InnerText);
            }
        }
    }
}
