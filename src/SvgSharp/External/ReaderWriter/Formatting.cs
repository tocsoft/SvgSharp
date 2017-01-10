// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace SharpSvg.System.Xml
{
    public enum Formatting
    {
        // No special formatting is done (this is the default).
        None,

        //This option causes child elements to be indented using the Indentation and IndentChar properties.  
        // It only indents Element Content (http://www.w3.org/TR/1998/REC-xml-19980210#sec-element-content)
        // and not Mixed Content (http://www.w3.org/TR/1998/REC-xml-19980210#sec-mixed-content)
        // according to the XML 1.0 definitions of these terms.
        Indented,
    };
}