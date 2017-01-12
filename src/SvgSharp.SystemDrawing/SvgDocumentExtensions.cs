using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Svg;

namespace SvgSharp.SystemDrawing
{
    public static class SvgDocumentExtensions
    {
        public static System.Drawing.Image Render(this SvgDocument document)
        {
            throw new NotImplementedException("System.Drawing Rendering not yet supported ");
        }
    }
}
