using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Svg
{
    /// <summary>
    /// Converts string representations of colours into <see cref="Color"/> objects.
    /// </summary>
    public class SvgColourConverter : SimpleBaseConverter<Color>
    {
        /// <summary>
        /// Converts the given object to the converter's native type.
        /// </summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor"/> that provides a format context. You can use this object to get additional information about the environment from which this converter is being invoked.</param>
        /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo"/> that specifies the culture to represent the color.</param>
        /// <param name="value">The object to convert.</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> representing the converted value.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The conversion cannot be performed.</exception>
        /// <PermissionSet>
        /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// </PermissionSet>
        public override Color Convert(string color)
        {
            if (color != null)
            {

                color = color.Trim();

                if (color.StartsWith("rgb"))
                {
                    try
                    {
                        int start = color.IndexOf("(") + 1;

                        //get the values from the RGB string
                        string[] values = color.Substring(start, color.IndexOf(")") - start).Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        //determine the alpha value if this is an RGBA (it will be the 4th value if there is one)
                        int alphaValue = 255;
                        if (values.Length > 3)
                        {
                            //the alpha portion of the rgba is not an int 0-255 it is a decimal between 0 and 1
                            //so we have to determine the corosponding byte value
                            var alphastring = values[3];
                            if (alphastring.StartsWith("."))
                            {
                                alphastring = "0" + alphastring;
                            }

                            var alphaDecimal = decimal.Parse(alphastring);

                            if (alphaDecimal <= 1)
                            {
                                alphaValue = (int)Math.Round(alphaDecimal * 255);
                            }
                            else
                            {
                                alphaValue = (int)Math.Round(alphaDecimal);
                            }
                        }

                        Color colorpart;
                        if (values[0].Trim().EndsWith("%"))
                        {
                            colorpart = new Color(alphaValue, (int)Math.Round(255 * float.Parse(values[0].Trim().TrimEnd('%')) / 100f),
                                                                                  (int)Math.Round(255 * float.Parse(values[1].Trim().TrimEnd('%')) / 100f),
                                                                                  (int)Math.Round(255 * float.Parse(values[2].Trim().TrimEnd('%')) / 100f));
                        }
                        else
                        {
                            colorpart = new Color(alphaValue, int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
                        }

                        return colorpart;
                    }
                    catch
                    {
                        throw new SvgException("Colour is in an invalid format: '" + color + "'");
                    }
                }
                // HSL support
                else if (color.StartsWith("hsl"))
                {
                    try
                    {
                        int start = color.IndexOf("(") + 1;

                        //get the values from the RGB string
                        string[] values = color.Substring(start, color.IndexOf(")") - start).Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (values[1].EndsWith("%"))
                        {
                            values[1] = values[1].TrimEnd('%');
                        }
                        if (values[2].EndsWith("%"))
                        {
                            values[2] = values[2].TrimEnd('%');
                        }
                        // Get the HSL values in a range from 0 to 1.
                        double h = double.Parse(values[0]) / 360.0;
                        double s = double.Parse(values[1]) / 100.0;
                        double l = double.Parse(values[2]) / 100.0;
                        // Convert the HSL color to an RGB color
                        Color colorpart = Hsl2Rgb(h, s, l);
                        return colorpart;
                    }
                    catch
                    {
                        throw new SvgException("Colour is in an invalid format: '" + color + "'");
                    }
                }
                else if (color.StartsWith("#") && color.Length == 4)
                {
                    return new Color(string.Format("#{0}{0}{1}{1}{2}{2}", color[1], color[2], color[3]));
                }


                //switch (color.ToLowerInvariant())
                //{
                //    case "activeborder": return SystemColors.ActiveBorder;
                //    case "activecaption": return SystemColors.ActiveCaption;
                //    case "appworkspace": return SystemColors.AppWorkspace;
                //    case "background": return SystemColors.Desktop;
                //    case "buttonface": return SystemColors.Control;
                //    case "buttonhighlight": return SystemColors.ControlLightLight;
                //    case "buttonshadow": return SystemColors.ControlDark;
                //    case "buttontext": return SystemColors.ControlText;
                //    case "captiontext": return SystemColors.ActiveCaptionText;
                //    case "graytext": return SystemColors.GrayText;
                //    case "highlight": return SystemColors.Highlight;
                //    case "highlighttext": return SystemColors.HighlightText;
                //    case "inactiveborder": return SystemColors.InactiveBorder;
                //    case "inactivecaption": return SystemColors.InactiveCaption;
                //    case "inactivecaptiontext": return SystemColors.InactiveCaptionText;
                //    case "infobackground": return SystemColors.Info;
                //    case "infotext": return SystemColors.InfoText;
                //    case "menu": return SystemColors.Menu;
                //    case "menutext": return SystemColors.MenuText;
                //    case "scrollbar": return SystemColors.ScrollBar;
                //    case "threeddarkshadow": return SystemColors.ControlDarkDark;
                //    case "threedface": return SystemColors.Control;
                //    case "threedhighlight": return SystemColors.ControlLight;
                //    case "threedlightshadow": return SystemColors.ControlLightLight;
                //    case "window": return SystemColors.Window;
                //    case "windowframe": return SystemColors.WindowFrame;
                //    case "windowtext": return SystemColors.WindowText;
                //}


            }
            return new Color(color);
        }

     

        /// <summary>
        /// Converts HSL color (with HSL specified from 0 to 1) to RGB color.
        /// Taken from http://www.geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
        /// </summary>
        /// <param name="h"></param>
        /// <param name="sl"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        private static Color Hsl2Rgb(double h, double sl, double l)
        {
            double r = l;   // default to gray
            double g = l;
            double b = l;
            double v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = l + l - v;
                sv = (v - m) / v;
                h *= 6.0;
                sextant = (int)h;
                fract = h - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            Color rgb = new Color((int)Math.Round(r * 255.0), (int)Math.Round(g * 255.0), (int)Math.Round(b * 255.0));
            return rgb;
        }
        public override string Convert(Color value)
        {
            return value.ToString();
        }
    }
}