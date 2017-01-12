using System;

using System.Collections.Generic;

namespace Svg
{
    public interface ISvgRenderer : IDisposable
    {
        float DpiY { get; }
        void DrawImage(IImage image, RectangleF destRect, RectangleF srcRect, GraphicsUnit graphicsUnit);
        void DrawImageUnscaled(IImage image, Point location);
        void DrawPath(IPen pen, IPath path);
        void FillPath(IBrush brush, IPath path);
        ISvgBoundable GetBoundable();
        IRegion GetClip();
        ISvgBoundable PopBoundable();
        void RotateTransform(float fAngle, MatrixOrder order = MatrixOrder.Append);
        void ScaleTransform(float sx, float sy, MatrixOrder order = MatrixOrder.Append);
        void SetBoundable(ISvgBoundable boundable);
        void SetClip(IRegion region, CombineMode combineMode = CombineMode.Replace);
        bool AntiAlias { get; set; }
        Matrix Transform { get; set; }
        void TranslateTransform(float dx, float dy, MatrixOrder order = MatrixOrder.Append);
    }

    public interface IPath
    {
        RectangleF GetBounds();
    }

    public interface IBrush
    {

    }

    public interface IPen
    {

    }

    //
    // Summary:
    //     Specifies the unit of measure for the given data.
    public enum GraphicsUnit
    {
        //
        // Summary:
        //     Specifies the world coordinate system unit as the unit of measure.
        World = 0,
        //
        // Summary:
        //     Specifies the unit of measure of the display device. Typically pixels for video
        //     displays, and 1/100 inch for printers.
        Display = 1,
        //
        // Summary:
        //     Specifies a device pixel as the unit of measure.
        Pixel = 2,
        //
        // Summary:
        //     Specifies a printer's point (1/72 inch) as the unit of measure.
        Point = 3,
        //
        // Summary:
        //     Specifies the inch as the unit of measure.
        Inch = 4,
        //
        // Summary:
        //     Specifies the document unit (1/300 inch) as the unit of measure.
        Document = 5,
        //
        // Summary:
        //     Specifies the millimeter as the unit of measure.
        Millimeter = 6
    }
    //
    // Summary:
    //     Specifies the order for matrix transform operations.
    public enum MatrixOrder
    {
        //
        // Summary:
        //     The new operation is applied before the old operation.
        Prepend = 0,
        //
        // Summary:
        //     The new operation is applied after the old operation.
        Append = 1
    }

    public interface IRegion
    {

    }

    //
    // Summary:
    //     Specifies how different clipping regions can be combined.
    public enum CombineMode
    {
        //
        // Summary:
        //     One clipping region is replaced by another.
        Replace = 0,
        //
        // Summary:
        //     Two clipping regions are combined by taking their intersection.
        Intersect = 1,
        //
        // Summary:
        //     Two clipping regions are combined by taking the union of both.
        Union = 2,
        //
        // Summary:
        //     Two clipping regions are combined by taking only the areas enclosed by one or
        //     the other region, but not both.
        Xor = 3,
        //
        // Summary:
        //     Specifies that the existing region is replaced by the result of the new region
        //     being removed from the existing region. Said differently, the new region is excluded
        //     from the existing region.
        Exclude = 4,
        //
        // Summary:
        //     Specifies that the existing region is replaced by the result of the existing
        //     region being removed from the new region. Said differently, the existing region
        //     is excluded from the new region.
        Complement = 5
    }
}
