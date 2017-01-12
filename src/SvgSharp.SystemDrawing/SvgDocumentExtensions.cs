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
        public static void RenderTo(this SvgDocument document, Bitmap bitmap)
        {
            try
            {
                using (var renderer = SvgRenderer.FromImage(bitmap))
                {
                    renderer.SetBoundable(new GenericBoundable(0, 0, bitmap.Width, bitmap.Height));

                    //EO, 2014-12-05: Requested to ensure proper zooming (draw the svg in the bitmap size, ==> proper scaling)
                    //EO, 2015-01-09, Added GetDimensions to use its returned size instead of this.Width and this.Height (request of Icarrere).
                    //BBN, 2015-07-29, it is unnecassary to call again GetDimensions and transform to 1x1
                    //JA, 2015-12-18, this is actually necessary to correctly render the Draw(rasterHeight, rasterWidth) overload, otherwise the rendered graphic doesn't scale correctly
                    var size = document.GetDimensions();
                    renderer.ScaleTransform(bitmap.Width / size.Width, bitmap.Height / size.Height);

                    //EO, 2014-12-05: Requested to ensure proper zooming out (reduce size). Otherwise it clip the image.
                    document.Overflow = SvgOverflow.Auto;

                    document.Draw(renderer);
                }
            }
            catch
            {
                throw;
            }
        }
        public static System.Drawing.Image Render(this SvgDocument document)
        {
            var size = document.GetDimensions();
            Bitmap bitmap = null;
            try
            {
                bitmap = new Bitmap((int)Math.Round(size.Width), (int)Math.Round(size.Height));
            }
            catch (ArgumentException e)
            {
                //When processing too many files at one the system can run out of memory
                //throw new SvgMemoryException("Cannot process SVG file, cannot allocate the required memory", e);
                throw;
            }

            // 	bitmap.SetResolution(300, 300);
            try
            {
                document.RenderTo(bitmap);
            }
            catch
            {
                bitmap.Dispose();
                throw;
            }

            //Trace.TraceInformation("End Render");
            return bitmap;
        }
    }
}
