using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Svg
{
    public struct RectangleF
    {
        public RectangleF(PointF location, SizeF size)
        {
            Location = location;
            Size = size;
        }

        public RectangleF(float x, float y, float width, float height)
            : this(new PointF (x,y), new SizeF(width, height))
        {
        }

        public float Height => Size.Height;
        public PointF Location { get; }
        public SizeF Size { get; }
        public float Top => Y;
        public float Bottom => Y + Height;

        public float Width => Size.Width;
        public float X => Location.X;
        public float Y => Location.Y;

        public float Left => X;
        public float Right => X + Width;


        public bool Contains(PointF rectangle)
        {
            throw new NotImplementedException();
        }
    }
    public struct PointF
    {
        public PointF(float x, float y) { X = x; Y = y; }

        public static PointF Empty = default(PointF);
        public float X { get; internal set; }
        public float Y { get; internal set; }

        public static PointF operator +(PointF p, SizeF s)
            => new PointF(p.X + s.Width, p.Y + s.Height);
    }
    public struct SizeF
    {
        public SizeF(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public float Height { get; internal set; }
        public float Width { get; internal set; }
    }

    public struct Matrix
    {
        public Matrix(params float[] values) : this()
        {
        }

        internal void Rotate(float angle)
        {
            throw new NotImplementedException();
        }

        public void Translate(float x, float y) { }
        public void Multiply(Matrix matrix) { }

        public float[] Elements { get; }

        internal void Scale(float x, float y)
        {
            throw new NotImplementedException();
        }

        internal void Shear(float x, float y)
        {
            throw new NotImplementedException();
        }
    }

}
