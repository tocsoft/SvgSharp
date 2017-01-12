using Svg.DataTypes;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Svg
{
    public sealed class SvgVisibilityConverter : SimpleBaseConverter<bool>
    {
        public override bool Convert(string value)
        {
            if (value == null)
            {
                return true;
            }

            if ((value == "hidden") || (value == "collapse"))
                return false;
            else
                return true;
        }
        public override string Convert(bool value)
        {
            return ((bool)value) ? "visible" : "hidden";
        }
    }

    //converts enums to lower case strings
    public class EnumBaseConverter<T> : SimpleBaseConverter<T>
        where T : struct
    {
        /// <summary>If specified, upon conversion, the default value will result in 'null'.</summary>
        public T? DefaultValue { get; protected set; }

        /// <summary>Creates a new instance.</summary>
        public EnumBaseConverter() { }

        /// <summary>Creates a new instance.</summary>
        /// <param name="defaultValue">Specified the default value of the enum.</param>
        public EnumBaseConverter(T defaultValue)
        {
            this.DefaultValue = defaultValue;
        }

        /// <summary>Attempts to convert the provided value to <typeparamref name="T"/>.</summary>
        public override T Convert(string value)
        {
            if (!(value is string))
            {
                throw new ArgumentOutOfRangeException("value must be a string.");
            }
            T result = default(T);
            if (Enum.TryParse<T>(value, true, out result))
            {
                return result;
            }

            return this.DefaultValue ?? default(T);
        }

        /// <summary>Attempts to convert the value to the destination type.</summary>
        public override string Convert(T value)
        {

            //If the value id the default value, no need to write the attribute.
            if (this.DefaultValue.HasValue && Enum.Equals(value, this.DefaultValue.Value))
                return null;
            else
            {
                //SVG attributes should be camelCase.
                string stringValue = value.ToString();

                stringValue = string.Format("{0}{1}", stringValue[0].ToString().ToLower(), stringValue.Substring(1));

                return stringValue;
            }
        }
    }

    public sealed class SvgFillRuleConverter : EnumBaseConverter<SvgFillRule>
    {
        public SvgFillRuleConverter() : base(SvgFillRule.NonZero) { }
    }

    public sealed class SvgColourInterpolationConverter : EnumBaseConverter<SvgColourInterpolation>
    {
        public SvgColourInterpolationConverter() : base(SvgColourInterpolation.SRGB) { }
    }

    public sealed class SvgClipRuleConverter : EnumBaseConverter<SvgClipRule>
    {
        public SvgClipRuleConverter() : base(SvgClipRule.NonZero) { }
    }

    public sealed class SvgTextAnchorConverter : EnumBaseConverter<SvgTextAnchor>
    {
        public SvgTextAnchorConverter() : base(SvgTextAnchor.Start) { }
    }

    public sealed class SvgStrokeLineCapConverter : EnumBaseConverter<SvgStrokeLineCap>
    {
        public SvgStrokeLineCapConverter() : base(SvgStrokeLineCap.Butt) { }
    }

    public sealed class SvgStrokeLineJoinConverter : EnumBaseConverter<SvgStrokeLineJoin>
    {
        public SvgStrokeLineJoinConverter() : base(SvgStrokeLineJoin.Miter) { }
    }

    public sealed class SvgMarkerUnitsConverter : EnumBaseConverter<SvgMarkerUnits>
    {
        public SvgMarkerUnitsConverter() : base(SvgMarkerUnits.StrokeWidth) { }
    }

    public sealed class SvgFontStyleConverter : EnumBaseConverter<SvgFontStyle>
    {
        public SvgFontStyleConverter() : base(SvgFontStyle.All) { }
    }

    public sealed class SvgOverflowConverter : EnumBaseConverter<SvgOverflow>
    {
        public SvgOverflowConverter() : base(SvgOverflow.Auto) { }
    }

    public sealed class SvgTextLengthAdjustConverter : EnumBaseConverter<SvgTextLengthAdjust>
    {
        public SvgTextLengthAdjustConverter() : base(SvgTextLengthAdjust.Spacing) { }
    }

    public sealed class SvgTextPathMethodConverter : EnumBaseConverter<SvgTextPathMethod>
    {
        public SvgTextPathMethodConverter() : base(SvgTextPathMethod.Align) { }
    }

    public sealed class SvgTextPathSpacingConverter : EnumBaseConverter<SvgTextPathSpacing>
    {
        public SvgTextPathSpacingConverter() : base(SvgTextPathSpacing.Exact) { }
    }

    public sealed class SvgShapeRenderingConverter : EnumBaseConverter<SvgShapeRendering>
    {
        public SvgShapeRenderingConverter() : base(SvgShapeRendering.Inherit) { }
    }

    public sealed class SvgTextRenderingConverter : EnumBaseConverter<SvgTextRendering>
    {
        public SvgTextRenderingConverter() : base(SvgTextRendering.Inherit) { }
    }

    public sealed class SvgImageRenderingConverter : EnumBaseConverter<SvgImageRendering>
    {
        public SvgImageRenderingConverter() : base(SvgImageRendering.Inherit) { }
    }

    public sealed class SvgFontVariantConverter : EnumBaseConverter<SvgFontVariant>
    {
        public SvgFontVariantConverter() : base(SvgFontVariant.Normal) { }

        public override SvgFontVariant Convert(string value)
        {
            if (value == "small-caps")
                return SvgFontVariant.Smallcaps;

            return base.Convert(value);
        }

        public override string Convert(SvgFontVariant value)
        {
            if (value == SvgFontVariant.Smallcaps)
            {
                return "small-caps";
            }

            return base.Convert(value);
        }
    }

    public sealed class SvgCoordinateUnitsConverter : EnumBaseConverter<SvgCoordinateUnits>
    {
        //TODO Inherit is not actually valid. See TODO on SvgCoordinateUnits enum.
        public SvgCoordinateUnitsConverter() : base(SvgCoordinateUnits.Inherit) { }
    }

    public sealed class SvgGradientSpreadMethodConverter : EnumBaseConverter<SvgGradientSpreadMethod>
    {
        public SvgGradientSpreadMethodConverter() : base(SvgGradientSpreadMethod.Pad) { }
    }

    public sealed class SvgTextDecorationConverter : EnumBaseConverter<SvgTextDecoration>
    {
        public SvgTextDecorationConverter() : base(SvgTextDecoration.None) { }

        public override SvgTextDecoration Convert(string value)
        {
            if (value.ToString() == "line-through")
                return SvgTextDecoration.LineThrough;

            return base.Convert(value);
        }

        public override string Convert(SvgTextDecoration value)
        {
            if (value == SvgTextDecoration.LineThrough)
            {
                return "line-through";
            }

            return base.Convert(value);
        }
    }

    public sealed class SvgFontWeightConverter : EnumBaseConverter<SvgFontWeight>
    {
        //TODO Defaulting to Normal although it should be All if this is used on a font face.
        public SvgFontWeightConverter() : base(SvgFontWeight.Normal) { }

        public override SvgFontWeight Convert(string value)
        {
            if (value is string)
            {
                switch ((string)value)
                {
                    case "100": return SvgFontWeight.W100;
                    case "200": return SvgFontWeight.W200;
                    case "300": return SvgFontWeight.W300;
                    case "400": return SvgFontWeight.W400;
                    case "500": return SvgFontWeight.W500;
                    case "600": return SvgFontWeight.W600;
                    case "700": return SvgFontWeight.W700;
                    case "800": return SvgFontWeight.W800;
                    case "900": return SvgFontWeight.W900;
                }
            }
            return base.Convert(value);
        }

        public override string Convert(SvgFontWeight value)
        {
            switch (value)
            {
                case SvgFontWeight.W100: return "100";
                case SvgFontWeight.W200: return "200";
                case SvgFontWeight.W300: return "300";
                case SvgFontWeight.W400: return "400";
                case SvgFontWeight.W500: return "500";
                case SvgFontWeight.W600: return "600";
                case SvgFontWeight.W700: return "700";
                case SvgFontWeight.W800: return "800";
                case SvgFontWeight.W900: return "900";
            }

            return base.Convert(value);
        }
    }

    //public static class Enums
    //{
    //    public static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct, IConvertible
    //    {
    //        var retValue = value == null ?
    //                    false :
    //                    Enum.IsDefined(typeof(TEnum), value);
    //        result = retValue ?
    //                    (TEnum)Enum.Parse(typeof(TEnum), value) :
    //                    default(TEnum);
    //        return retValue;
    //    }
    //}
}
