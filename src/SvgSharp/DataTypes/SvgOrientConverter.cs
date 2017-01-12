using System;
using System.ComponentModel;
using System.Globalization;

namespace Svg.DataTypes
{
    public sealed class SvgOrientConverter : SimpleBaseConverter<SvgOrient>
    {
        public override SvgOrient Convert(string value)
        {
            if (value == null)
            {
                return new SvgOrient();
            }
            
            switch (value.ToString())
            {
                case "auto":
                    return (new SvgOrient());
                default:
                    float fTmp = float.MinValue;
                    if (!float.TryParse(value.ToString(), out fTmp))
                        throw new ArgumentOutOfRangeException("value must be a valid float.");
                    return (new SvgOrient(fTmp));
            }
        }


        public override string Convert(SvgOrient value)
        {
            if (value == null)
            {
                return null;
            }


            return value.ToString();

            //switch (value.ToString())
            //{
            //    case "auto":
            //        return (new SvgOrient());
            //    default:
            //        float fTmp = float.MinValue;
            //        if (!float.TryParse(value.ToString(), out fTmp))
            //            throw new ArgumentOutOfRangeException("value must be a valid float.");
            //        return (new SvgOrient(fTmp));
            //}
        }
    }
}
