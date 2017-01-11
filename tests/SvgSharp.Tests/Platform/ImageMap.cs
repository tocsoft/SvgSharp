using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageSharp.Tests.Platform
{
    public static class ImageMap
    {
#if NET_CORE
        public static INormalImage AsImage(this ImageSharp.Image img)
        {
            throw new NotImplementedException();
        }
#else
        public static INormalImage AsImage(this System.Drawing.Image img)
        {
            throw new NotImplementedException();
        }
#endif
    }
}
