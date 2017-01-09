using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace Svg
{
    /// <summary>
    /// An SVG element to render circles to the document.
    /// </summary>
    [SvgElement("circle")]
    public class SvgCircle : SvgVisualElement
    {
        private SvgUnit _radius;
        private SvgUnit _centerX;
        private SvgUnit _centerY;

        /// <summary>
        /// Gets the center point of the circle.
        /// </summary>
        /// <value>The center.</value>
        public SvgPoint Center
        {
            get { return new SvgPoint(this.CenterX, this.CenterY); }
        }

        /// <summary>
        /// Gets or sets the center x.
        /// </summary>
        /// <value>
        /// The center x.
        /// </value>
        [SvgAttribute("cx")]
        public virtual SvgUnit CenterX
        {
            get { return this._centerX; }
            set
            {
            	if(_centerX != value)
            	{
            		this._centerX = value;
            		this.IsPathDirty = true;
            		OnAttributeChanged(new AttributeEventArgs{ Attribute = "cx", Value = value });
            	}
            }
        }

        [SvgAttribute("cy")]
        public virtual SvgUnit CenterY
        {
        	get { return this._centerY; }
        	set
        	{
        		if(_centerY != value)
        		{
        			this._centerY = value;
        			this.IsPathDirty = true;
        			OnAttributeChanged(new AttributeEventArgs{ Attribute = "cy", Value = value });
        		}
        	}
        }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        [SvgAttribute("r")]
        public virtual SvgUnit Radius
        {
        	get { return this._radius; }
        	set
        	{
        		if(_radius != value)
        		{
        			this._radius = value;
        			this.IsPathDirty = true;
        			OnAttributeChanged(new AttributeEventArgs{ Attribute = "r", Value = value });
        		}
        	}
        }

        /// <summary>
        /// Gets the bounds of the circle.
        /// </summary>
        /// <value>The rectangular bounds of the circle.</value>
        public override RectangleF Bounds
        {
            get
            {
                throw new NotImplementedException();
                //return this.Path(null).GetBounds();
            }
        }

        // /// <summary>
        // /// Gets the <see cref="GraphicsPath"/> representing this element.
        // /// </summary>
        // public override GraphicsPath Path(ISvgRenderer renderer)
        // {
        //     if (this._path == null || this.IsPathDirty)
        //     {
        //float halfStrokeWidth = base.StrokeWidth / 2;

        //// If it is to render, don't need to consider stroke width.
        //// i.e stroke width only to be considered when calculating boundary
        //if (renderer != null)
        //{
        //	halfStrokeWidth = 0;
        //	this.IsPathDirty = false;
        //}

        //         _path = new GraphicsPath();
        //         _path.StartFigure();
        //	var center = this.Center.ToDeviceValue(renderer, this);
        //	var radius = this.Radius.ToDeviceValue(renderer, UnitRenderingType.Other, this) + halfStrokeWidth;
        //	_path.AddEllipse(center.X - radius, center.Y - radius, 2 * radius, 2 * radius);
        //         _path.CloseFigure();
        //     }
        //     return _path;
        // }

        /// <summary>
        /// Renders the circle to the specified <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="renderer">The <see cref="ISvgRenderer" /> object to render to.</param>
        protected override void Render(ISvgRenderer renderer)
        {
            // Don't draw if there is no radius set
            if (this.Radius.Value > 0.0f)
            {
                base.Render(renderer);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SvgCircle"/> class.
        /// </summary>
        public SvgCircle()
        {
            CenterX = new SvgUnit(0.0f);
            CenterY = new SvgUnit(0.0f);
        }


        /// <summary>
        /// Deeps the copy.
        /// </summary>
        /// <returns></returns>
        public override SvgElement DeepCopy()
		{
			return DeepCopy<SvgCircle>();
		}

        /// <summary>
        /// Deeps the copy.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override SvgElement DeepCopy<T>()
		{
			var newObj = base.DeepCopy<T>() as SvgCircle;
			newObj.CenterX = this.CenterX;
			newObj.CenterY = this.CenterY;
			newObj.Radius = this.Radius;
			return newObj;
		}
    }
}