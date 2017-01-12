using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Svg;

namespace SvgSharp
{
    public class SystemDrawingRegion : IRegion
    {
        public SystemDrawingRegion(Region region)
        {
            this.Region = region;
        }

        public Region Region { get; private set; }
    }
}
