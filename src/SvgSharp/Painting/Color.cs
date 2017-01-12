using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Svg
{
    public struct Color
    {
        public string _innerColorString;
        private int alphaValue;
        private int red;
        private int green;
        private int blue;

        public Color(string value)
            :this(0,0,0,0)
        {
            _innerColorString = value;
        }

        public Color(int red, int green, int blue) : this(255, red, green, blue)
        {
        }

        public Color(int alphaValue, int red, int green, int blue) : this()
        {
            this.alphaValue = alphaValue;
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public override string ToString()
        {
            if(_innerColorString == null)
            {
                return $"#{red:X}{green:X}{blue:X}{alphaValue:X}";
            }

            return _innerColorString;
        }
    }
}
