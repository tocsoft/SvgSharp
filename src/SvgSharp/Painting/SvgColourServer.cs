using System;
using System.Collections.Generic;
using System.Text;
//using System.Drawing;

namespace Svg
{
    public sealed class SvgColorServer : SvgPaintServer
    {

        /// <summary>
        /// An unspecified <see cref="SvgPaintServer"/>.
        /// </summary>
        public static readonly SvgPaintServer NotSet;//= new SvgColourServer(System.Drawing.Color.Black);
        /// <summary>
        /// A <see cref="SvgPaintServer"/> that should inherit from its parent.
        /// </summary>
        public static readonly SvgPaintServer Inherit;// = new SvgColourServer(System.Drawing.Color.Black);

        public SvgColorServer()
            : this("black")
        {
        }

        public SvgColorServer(string color)
            : this(new Color(color))
        {
        }

        public SvgColorServer(Color color)
        {
            this._color = color;
        }

        private Color _color;

        public Color ActualColor
        {
            get { return this._color; }
            set { this._color = value; }
        }

        //public override Brush GetBrush(SvgVisualElement styleOwner, ISvgRenderer renderer, float opacity, bool forStroke = false)
        //{
        //    //is none?
        //    if (this == SvgPaintServer.None) return new SolidBrush(System.Drawing.Color.Transparent);

        //    int alpha = (int)Math.Round((opacity * (this.Colour.A/255.0) ) * 255);
        //    Color colour = System.Drawing.Color.FromArgb(alpha, this.Colour);

        //    return new SolidBrush(colour);
        //}

        public override string ToString()
        {
        	if(this == SvgPaintServer.None)
        		return "none";
        	else if(this == SvgColorServer.NotSet)
        		return "";

            return this.ActualColor.ToString();

            //// Return the name if it exists
            //if (c.IsKnownColor)
            //{
            //    return c.Name;
            //}

            //// Return the hex value
            //return String.Format("#{0}", c.ToArgb().ToString("x").Substring(2));
        }


		public override SvgElement DeepCopy()
		{
			return DeepCopy<SvgColorServer>();
		}


		public override SvgElement DeepCopy<T>()
        {
            var newObj = base.DeepCopy<T>() as SvgColorServer;
			newObj.ActualColor = this.ActualColor;
			return newObj;

		}

        public override bool Equals(object obj)
        {
            var objColor = obj as SvgColorServer;
            if (objColor == null)
                return false;

            if ((this == SvgPaintServer.None && obj != SvgPaintServer.None) ||
                (this != SvgPaintServer.None && obj == SvgPaintServer.None) ||
                (this == SvgColorServer.NotSet && obj != SvgColorServer.NotSet) ||
                (this != SvgColorServer.NotSet && obj == SvgColorServer.NotSet) ||
                (this == SvgColorServer.Inherit && obj != SvgColorServer.Inherit) ||
                (this != SvgColorServer.Inherit && obj == SvgColorServer.Inherit)) return false;

            return this.GetHashCode() == objColor.GetHashCode();
        }

        public override int GetHashCode()
        {
            return _color.GetHashCode();
        }
    }
}
