using Svg.Css;
using System;
using ExCSS;
using Xunit;
using System.Collections;

namespace Svg.UnitTests
{
	/// <summary>
	///This is a test class for CssQueryTest and is intended
	///to contain all CssQueryTest Unit Tests
	///</summary>
    public class CssQueryTest
    {
        public static TheoryData<string, int> specificyTests = new TheoryData<string, int> {
            { "*", 0x0 }
            ,{ "*", 0x0 }
            ,{ "li", 0x10 }
            ,{ "li:first-line", 0x20 }
            ,{ "ul li", 0x20 }
            ,{ "ul ol+li", 0x30 }
            ,{ "h1 + *[rel=up]", 0x110 }
            ,{ "ul ol li.red", 0x130 }
            ,{ "li.red.level", 0x210 }
            ,{ "p", 0x010 }
            ,{ "div p", 0x020 }
            ,{ ".sith", 0x100 }
            ,{ "div p.sith", 0x120 }
            ,{ "#sith", 0x1000 }
            ,{ "body #darkside .sith p", 0x1120 }
            ,{ "body #content .data img:hover", 0x1220 }
            ,{ "a#a-02", 0x1010 }
            ,{ "a[id=\"a-02\"]", 0x0110 }
            ,{ "ul#nav li.active a", 0x1130 }
            ,{ "body.ie7 .col_3 h2 ~ h2", 0x0230 }
            ,{ "#footer *:not(nav) li", 0x1020 }
            ,{ "ul > li ul li ol li:first-letter", 0x0070 }
        };

        /// <summary>
        ///A test for GetSpecificity
        ///</summary>
        ///<remarks>Lifted from http://www.smashingmagazine.com/2007/07/27/css-specificity-things-you-should-know/, and http://css-tricks.com/specifics-on-css-specificity/ </remarks>
        [Theory]
        [MemberData(nameof(specificyTests))]
        public void RunSpecificityTests(string selector, int specificity)
        {
            var parser = new ExCSS.Parser();
            var sheet = parser.Parse(selector + " {color:black}");
            Assert.Equal(specificity, CssQuery.GetSpecificity(sheet.StyleRules[0].Selector));
        }
    }
}
