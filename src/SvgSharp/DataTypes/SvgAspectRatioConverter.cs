using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Svg.DataTypes
{

    //implementaton for preserve aspect ratio
    public sealed class SvgAspectRatioConverter : SimpleBaseConverter<SvgAspectRatio>
    {
        public override SvgAspectRatio Convert(string value)
        {
            if (value == null)
            {
                return new SvgAspectRatio(SvgPreserveAspectRatio.none);
            }

            if (!(value is string))
            {
                throw new ArgumentOutOfRangeException("value must be a string.");
            }

            SvgPreserveAspectRatio eAlign = SvgPreserveAspectRatio.none;
            bool bDefer = false;
            bool bSlice = false;

            string[] sParts = (value as string).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int nAlignIndex = 0;
            if (sParts[0].Equals("defer"))
            {
                bDefer = true;
                nAlignIndex++;
                if (sParts.Length < 2)
                    throw new ArgumentOutOfRangeException("value is not a member of SvgPreserveAspectRatio");
            }
            
            if (!Enum.TryParse(sParts[nAlignIndex], true, out eAlign))
                throw new ArgumentOutOfRangeException("value is not a member of SvgPreserveAspectRatio");

            nAlignIndex++;

            if (sParts.Length > nAlignIndex)
            {
                switch (sParts[nAlignIndex])
                {
                    case "meet":
                        break;
                    case "slice":
                        bSlice = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value is not a member of SvgPreserveAspectRatio");
                }
            }
            nAlignIndex++;
            if (sParts.Length > nAlignIndex)
                throw new ArgumentOutOfRangeException("value is not a member of SvgPreserveAspectRatio");

            SvgAspectRatio pRet = new SvgAspectRatio(eAlign);
            pRet.Slice = bSlice;
            pRet.Defer = bDefer;
            return (pRet);
        }
        
        public override string Convert(SvgAspectRatio value)
        {
            return value?.ToString();
        }
    }
}
