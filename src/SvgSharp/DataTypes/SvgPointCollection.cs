using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace Svg
{
    /// <summary>
    /// Represents a list of <see cref="SvgUnits"/> used with the <see cref="SvgPolyline"/> and <see cref="SvgPolygon"/>.
    /// </summary>
    [TypeConverter(typeof(SvgPointCollectionConverter))]
    public class SvgPointCollection : List<SvgUnit>
    {
        public override string ToString()
        {
            var builder = new StringBuilder();
            for (var i = 0; i < Count; i += 2) 
            {
                if (i + 1 < Count) 
                {
                    if (i > 1) 
                    {
                        builder.Append(" ");
                    }
                    // we don't need unit type
                    builder.Append(this[i].Value.ToString(CultureInfo.InvariantCulture));
                    builder.Append(",");
                    builder.Append(this[i + 1].Value.ToString(CultureInfo.InvariantCulture));
                }    
            }
            return builder.ToString();
        }
    }

    /// <summary>
    /// A class to convert string into <see cref="SvgUnitCollection"/> instances.
    /// </summary>
    internal class SvgPointCollectionConverter : SimpleBaseConverter<SvgPointCollection>
    {
        private static readonly SvgUnitConverter _unitConverter = new SvgUnitConverter();


        /// <summary>
        /// Converts the given object to the type of this converter, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The <see cref="T:System.Object"/> to convert.</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> that represents the converted value.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
        public override SvgPointCollection Convert(string value)
        {

            var strValue = ((string)value).Trim();
            if (string.Compare(strValue, "none", StringComparison.OrdinalIgnoreCase) == 0) return null;

            var parser = new CoordinateParser(strValue);
            var pointValue = 0.0f;
            var result = new SvgPointCollection();
            while (parser.TryGetFloat(out pointValue))
            {
                result.Add(new SvgUnit(SvgUnitType.User, pointValue));
            }

            return result;
        }

        public override string Convert(SvgPointCollection value)
        {
            return value?.ToString();
        }
    }
}
