using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using Svg;

namespace SvgSharp
{
    /// <summary>
    /// Convenience wrapper around a graphics object
    /// </summary>
    public sealed class SvgRenderer : IDisposable, IGraphicsProvider, ISvgRenderer
    {
        private Graphics _innerGraphics;
        private Stack<ISvgBoundable> _boundables = new Stack<ISvgBoundable>();

        public void SetBoundable(ISvgBoundable boundable)
        {
            _boundables.Push(boundable);
        }
        public ISvgBoundable GetBoundable()
        {
            return _boundables.Peek();
        }
        public ISvgBoundable PopBoundable()
        {
            return _boundables.Pop();
        }

        public float DpiY
        {
            get { return _innerGraphics.DpiY; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ISvgRenderer"/> class.
        /// </summary>
        private SvgRenderer(Graphics graphics)
        {
            this._innerGraphics = graphics;
        }

        public void DrawImage(IImage image, Svg.RectangleF destRect, Svg.RectangleF srcRect, Svg.GraphicsUnit graphicsUnit)
        {
            throw new NotImplementedException();
            //we now translate these back to real word items

          //  _innerGraphics.DrawImage(image, destRect, srcRect, graphicsUnit);
        }
        public void DrawImageUnscaled(IImage image, Svg.Point location)
        {
            throw new NotImplementedException();
            //this._innerGraphics.DrawImageUnscaled(image, location);
        }
        public void DrawPath(IPen pen, IPath path)
            
        {
            throw new NotImplementedException();
            //this._innerGraphics.DrawPath(pen, path);
        }
        public void FillPath(IBrush brush, IPath path)
        {
            throw new NotImplementedException();
            //this._innerGraphics.FillPath(brush, path);
        }
        public Svg.IRegion GetClip()
        {
            return new SystemDrawingRegion(this._innerGraphics.Clip);
        }
        public void RotateTransform(float fAngle, Svg.MatrixOrder order = Svg.MatrixOrder.Append)
        {
            this._innerGraphics.RotateTransform(fAngle, (System.Drawing.Drawing2D.MatrixOrder)(int)order);
        }
        public void ScaleTransform(float sx, float sy, Svg.MatrixOrder order = Svg.MatrixOrder.Append)
        {
            this._innerGraphics.ScaleTransform(sx, sy, (System.Drawing.Drawing2D.MatrixOrder)(int)order);
        }

        public void SetClip(IRegion region, Svg.CombineMode combineMode = Svg.CombineMode.Replace)
        {
            var reg = (region as SystemDrawingRegion)?.Region;

            if (reg == null && region is Svg.RectangleF)
            {
                var rect = (Svg.RectangleF)region;
                reg = new Region(new System.Drawing.RectangleF(rect.X, rect.Y, rect.Width, rect.Height));
            }

            
            this._innerGraphics.SetClip(reg, (System.Drawing.Drawing2D.CombineMode)(int)combineMode);
        }

        public void TranslateTransform(float dx, float dy, Svg.MatrixOrder order = Svg.MatrixOrder.Append)
        {
            this._innerGraphics.TranslateTransform(dx, dy, (System.Drawing.Drawing2D.MatrixOrder)(int)order);
        }
        
        public Svg.Matrix Transform
        {
            get
            {
                var elms = this._innerGraphics.Transform.Elements;
                return new Svg.Matrix(elms[0], elms[1], elms[2], elms[3], elms[4], elms[5]);
            }

            set
            {
                var elms = value.Elements;
                _innerGraphics.Transform = new System.Drawing.Drawing2D.Matrix(elms[0], elms[1], elms[2], elms[3], elms[4], elms[5]);

            }
        }

        public bool AntiAlias
        {
            get
            {
                switch (this._innerGraphics.SmoothingMode)
                {
                    case SmoothingMode.HighQuality:
                    case SmoothingMode.AntiAlias:
                        return true;
                    default:
                        return false;
                }
            }
            set
            {
                this._innerGraphics.SmoothingMode = value ? SmoothingMode.AntiAlias : SmoothingMode.None;
            }
        }

        public void Dispose()
        {
            this._innerGraphics.Dispose();
        }

        //Graphics IGraphicsProvider.GetGraphics()
        //{
        //    return _innerGraphics;
        //}

        /// <summary>
        /// Creates a new <see cref="ISvgRenderer"/> from the specified <see cref="Image"/>.
        /// </summary>
        /// <param name="image"><see cref="Image"/> from which to create the new <see cref="ISvgRenderer"/>.</param>
        public static ISvgRenderer FromImage(Image image)
        {
            var g = Graphics.FromImage(image);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.TextContrast = 1;
            return new SvgRenderer(g);
        }

        /// <summary>
        /// Creates a new <see cref="ISvgRenderer"/> from the specified <see cref="Graphics"/>.
        /// </summary>
        /// <param name="graphics">The <see cref="Graphics"/> to create the renderer from.</param>
        public static ISvgRenderer FromGraphics(Graphics graphics)
        {
            return new SvgRenderer(graphics);
        }

        public static ISvgRenderer FromNull()
        {
            var img = new Bitmap(1, 1);
            return SvgRenderer.FromImage(img);
        }
    }
}
