using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Svg
{
    /// <summary>
    /// Represents a list of <see cref="SvgUnits"/>.
    /// </summary>
    [TypeConverter(typeof(SvgUnitCollectionConverter))]
    public class SvgUnitCollection : List<SvgUnit>
    {
        public override string ToString()
        {
            // The correct separator should be a single white space.
            // More see:
            // http://www.w3.org/TR/SVG/coords.html
            // "Superfluous white space and separators such as commas can be eliminated
            // (e.g., 'M 100 100 L 200 200' contains unnecessary spaces and could be expressed more compactly as 'M100 100L200 200')."
            // http://www.w3.org/TR/SVGTiny12/paths.html#PathDataGeneralInformation
            // https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/d#Notes
#if Net4
            return String.Join(" ", this.Select(u => u.ToString()));
#else
            return String.Join(" ", this.Select(u => u.ToString()).ToArray());
#endif
        }

        public static bool IsNullOrEmpty(SvgUnitCollection collection)
        {
            return collection == null || collection.Count < 1 ||
                (collection.Count == 1 && (collection[0] == SvgUnit.Empty || collection[0] == SvgUnit.None));
        }
    }

    /// <summary>
    /// A class to convert string into <see cref="SvgUnitCollection"/> instances.
    /// </summary>
    internal class SvgUnitCollectionConverter : SimpleBaseConverter<SvgUnitCollection>
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
        public override SvgUnitCollection Convert(string value)
        {
                if (string.Compare(((string)value).Trim(), "none", StringComparison.OrdinalIgnoreCase) == 0) return null;
                string[] points = ((string)value).Trim().Split(new char[] { ',', ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                SvgUnitCollection units = new SvgUnitCollection();

                foreach (string point in points)
                {
                    SvgUnit newUnit = _unitConverter.Convert(point.Trim());
                    if (!newUnit.IsNone)
                        units.Add(newUnit);
                }

                return units;
        }
        
        public override string Convert(SvgUnitCollection value)
        {
            return value?.ToString();
        }
    }
}