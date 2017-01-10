// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Globalization;

namespace SharpSvg.System.Xml
{
    internal class XmlExceptionHelper
    {
        internal static string[] BuildCharExceptionArgs(string data, int invCharIndex)
        {
            return BuildCharExceptionArgs(data[invCharIndex], invCharIndex + 1 < data.Length ? data[invCharIndex + 1] : '\0');
        }

        internal static string[] BuildCharExceptionArgs(char[] data, int invCharIndex)
        {
            return BuildCharExceptionArgs(data, data.Length, invCharIndex);
        }

        internal static string[] BuildCharExceptionArgs(char[] data, int length, int invCharIndex)
        {
            Debug.Assert(invCharIndex < data.Length);
            Debug.Assert(invCharIndex < length);
            Debug.Assert(length <= data.Length);

            return BuildCharExceptionArgs(data[invCharIndex], invCharIndex + 1 < length ? data[invCharIndex + 1] : '\0');
        }

        internal static string[] BuildCharExceptionArgs(char invChar, char nextChar)
        {
            string[] aStringList = new string[2];

            // for surrogate characters include both high and low char in the message so that a full character is displayed
            if (XmlCharType.IsHighSurrogate(invChar) && nextChar != 0)
            {
                int combinedChar = XmlCharType.CombineSurrogateChar(nextChar, invChar);
                aStringList[0] = new string(new char[] { invChar, nextChar });
                aStringList[1] = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", combinedChar);
            }
            else
            {
                // don't include 0 character in the string - in means eof-of-string in native code, where this may bubble up to
                if ((int)invChar == 0)
                {
                    aStringList[0] = ".";
                }
                else
                {
                    aStringList[0] = Convert.ToString(invChar, CultureInfo.InvariantCulture);
                }
                aStringList[1] = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", (int)invChar);
            }
            return aStringList;
        }
    }
}