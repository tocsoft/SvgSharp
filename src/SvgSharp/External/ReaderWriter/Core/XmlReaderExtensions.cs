// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using SharpSvg.System.Xml;

namespace SharpSvg.System.Xml { 
    public class XmlReaderExtensions
    {
        static private uint s_isTextualNodeBitmap = 0x6018; // 00 0110 0000 0001 1000
                                                            // 0 None, 
                                                            // 0 Element,
                                                            // 0 Attribute,
                                                            // 1 Text,
                                                            // 1 CDATA,
                                                            // 0 EntityReference,
                                                            // 0 Entity,
                                                            // 0 ProcessingInstruction,
                                                            // 0 Comment,
                                                            // 0 Document,
                                                            // 0 DocumentType,
                                                            // 0 DocumentFragment,
                                                            // 0 Notation,
                                                            // 1 Whitespace,
                                                            // 1 SignificantWhitespace,
                                                            // 0 EndElement,
                                                            // 0 EndEntity,
                                                            // 0 XmlDeclaration

        static private uint s_canReadContentAsBitmap = 0x1E1BC; // 01 1110 0001 1011 1100
                                                                // 0 None, 
                                                                // 0 Element,
                                                                // 1 Attribute,
                                                                // 1 Text,
                                                                // 1 CDATA,
                                                                // 1 EntityReference,
                                                                // 0 Entity,
                                                                // 1 ProcessingInstruction,
                                                                // 1 Comment,
                                                                // 0 Document,
                                                                // 0 DocumentType,
                                                                // 0 DocumentFragment,
                                                                // 0 Notation,
                                                                // 1 Whitespace,
                                                                // 1 SignificantWhitespace,
                                                                // 1 EndElement,
                                                                // 1 EndEntity,
                                                                // 0 XmlDeclaration

        static private uint s_hasValueBitmap = 0x2659C; // 10 0110 0101 1001 1100
                                                        // 0 None, 
                                                        // 0 Element,
                                                        // 1 Attribute,
                                                        // 1 Text,
                                                        // 1 CDATA,
                                                        // 0 EntityReference,
                                                        // 0 Entity,
                                                        // 1 ProcessingInstruction,
                                                        // 1 Comment,
                                                        // 0 Document,
                                                        // 1 DocumentType,
                                                        // 0 DocumentFragment,
                                                        // 0 Notation,
                                                        // 1 Whitespace,
                                                        // 1 SignificantWhitespace,
                                                        // 0 EndElement,
                                                        // 0 EndEntity,
                                                        // 1 XmlDeclaration

        internal const int DefaultBufferSize = 4096;
        internal const int BiggerBufferSize = 8192;
        internal const int MaxStreamLengthForDefaultBufferSize = 64 * 1024; // 64kB

        internal const int AsyncBufferSize = 64 * 1024; //64KB


        static internal bool IsTextualNode(XmlNodeType nodeType)
        {
    #if DEBUG
                // This code verifies IsTextualNodeBitmap mapping of XmlNodeType to a bool specifying
                // whether the node is 'textual' = Text, CDATA, Whitespace or SignificantWhitespace.
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.None)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.Element)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.Attribute)));
                Debug.Assert(0 != (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.Text)));
                Debug.Assert(0 != (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.CDATA)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.EntityReference)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.Entity)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.ProcessingInstruction)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.Comment)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.Document)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.DocumentType)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.DocumentFragment)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.Notation)));
                Debug.Assert(0 != (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.Whitespace)));
                Debug.Assert(0 != (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.SignificantWhitespace)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.EndElement)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.EndEntity)));
                Debug.Assert(0 == (s_isTextualNodeBitmap & (1 << (int)XmlNodeType.XmlDeclaration)));
    #endif
            return 0 != (s_isTextualNodeBitmap & (1 << (int)nodeType));
        }


        static internal bool CanReadContentAs(XmlNodeType nodeType)
        {
    #if DEBUG
                // This code verifies IsTextualNodeBitmap mapping of XmlNodeType to a bool specifying
                // whether ReadContentAsXxx calls are allowed on his node type
                Debug.Assert(0 == (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.None)));
                Debug.Assert(0 == (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.Element)));
                Debug.Assert(0 != (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.Attribute)));
                Debug.Assert(0 != (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.Text)));
                Debug.Assert(0 != (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.CDATA)));
                Debug.Assert(0 != (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.EntityReference)));
                Debug.Assert(0 == (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.Entity)));
                Debug.Assert(0 != (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.ProcessingInstruction)));
                Debug.Assert(0 != (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.Comment)));
                Debug.Assert(0 == (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.Document)));
                Debug.Assert(0 == (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.DocumentType)));
                Debug.Assert(0 == (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.DocumentFragment)));
                Debug.Assert(0 == (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.Notation)));
                Debug.Assert(0 != (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.Whitespace)));
                Debug.Assert(0 != (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.SignificantWhitespace)));
                Debug.Assert(0 != (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.EndElement)));
                Debug.Assert(0 != (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.EndEntity)));
                Debug.Assert(0 == (s_canReadContentAsBitmap & (1 << (int)XmlNodeType.XmlDeclaration)));
    #endif
            return 0 != (s_canReadContentAsBitmap & (1 << (int)nodeType));
        }

        static internal bool HasValueInternal(XmlNodeType nodeType)
        {
    #if DEBUG
                // This code verifies HasValueBitmap mapping of XmlNodeType to a bool specifying
                // whether the node can have a non-empty Value
                Debug.Assert(0 == (s_hasValueBitmap & (1 << (int)XmlNodeType.None)));
                Debug.Assert(0 == (s_hasValueBitmap & (1 << (int)XmlNodeType.Element)));
                Debug.Assert(0 != (s_hasValueBitmap & (1 << (int)XmlNodeType.Attribute)));
                Debug.Assert(0 != (s_hasValueBitmap & (1 << (int)XmlNodeType.Text)));
                Debug.Assert(0 != (s_hasValueBitmap & (1 << (int)XmlNodeType.CDATA)));
                Debug.Assert(0 == (s_hasValueBitmap & (1 << (int)XmlNodeType.EntityReference)));
                Debug.Assert(0 == (s_hasValueBitmap & (1 << (int)XmlNodeType.Entity)));
                Debug.Assert(0 != (s_hasValueBitmap & (1 << (int)XmlNodeType.ProcessingInstruction)));
                Debug.Assert(0 != (s_hasValueBitmap & (1 << (int)XmlNodeType.Comment)));
                Debug.Assert(0 == (s_hasValueBitmap & (1 << (int)XmlNodeType.Document)));
                Debug.Assert(0 != (s_hasValueBitmap & (1 << (int)XmlNodeType.DocumentType)));
                Debug.Assert(0 == (s_hasValueBitmap & (1 << (int)XmlNodeType.DocumentFragment)));
                Debug.Assert(0 == (s_hasValueBitmap & (1 << (int)XmlNodeType.Notation)));
                Debug.Assert(0 != (s_hasValueBitmap & (1 << (int)XmlNodeType.Whitespace)));
                Debug.Assert(0 != (s_hasValueBitmap & (1 << (int)XmlNodeType.SignificantWhitespace)));
                Debug.Assert(0 == (s_hasValueBitmap & (1 << (int)XmlNodeType.EndElement)));
                Debug.Assert(0 == (s_hasValueBitmap & (1 << (int)XmlNodeType.EndEntity)));
                Debug.Assert(0 != (s_hasValueBitmap & (1 << (int)XmlNodeType.XmlDeclaration)));
    #endif
            return 0 != (s_hasValueBitmap & (1 << (int)nodeType));
        }

        internal static int CalcBufferSize(Stream input)
        {
            // determine the size of byte buffer
            int bufferSize = DefaultBufferSize;
            if (input.CanSeek)
            {
                long len = input.Length;
                if (len < bufferSize)
                {
                    bufferSize = checked((int)len);
                }
                else if (len > MaxStreamLengthForDefaultBufferSize)
                {
                    bufferSize = BiggerBufferSize;
                }
            }

            // return the byte buffer size
            return bufferSize;
        }

        internal static ConformanceLevel GetV1ConformanceLevel(XmlReader reader)
        {
            XmlTextReaderImpl tri = GetXmlTextReaderImpl(reader);
            return tri != null ? tri.V1ComformanceLevel : ConformanceLevel.Document;
        }

        private static XmlTextReaderImpl GetXmlTextReaderImpl(XmlReader reader)
        {
            XmlTextReaderImpl tri = reader as XmlTextReaderImpl;
            if (tri != null)
            {
                return tri;
            }
            return null;
        }

    }
}