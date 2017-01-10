using System.IO;
using System.Xml;

namespace SharpSvg.System.Xml
{
    public class XmlTextReader : XmlReader, IXmlLineInfo
    {
        private readonly XmlTextReaderImpl _implementation;

        public XmlTextReader(string fileName, TextReader textReader, bool normalizeLineEndings)
        {
            _implementation = new XmlTextReaderImpl(fileName, textReader, normalizeLineEndings);
        }

        public XmlTextReader(StringReader stringReader, bool normalizeLineEndings)
        {
            _implementation = new XmlTextReaderImpl(stringReader, normalizeLineEndings);
        }

        public override string GetAttribute(int i)
        {
            return _implementation.GetAttribute(i);
        }

        public override string GetAttribute(string name)
        {
            return _implementation.GetAttribute(name);
        }

        public override string GetAttribute(string name, string namespaceURI)
        {
            return _implementation.GetAttribute(name, namespaceURI);
        }

        public override string LookupNamespace(string prefix)
        {
            return _implementation.LookupNamespace(prefix);
        }

        public override bool MoveToAttribute(string name)
        {
            return _implementation.MoveToAttribute(name);
        }

        public override bool MoveToAttribute(string name, string ns)
        {
            return _implementation.MoveToAttribute(name, ns);
        }

        public override bool MoveToElement()
        {
            return _implementation.MoveToElement();
        }

        public override bool MoveToFirstAttribute()
        {
            return _implementation.MoveToFirstAttribute();
        }

        public override bool MoveToNextAttribute()
        {
            return _implementation.MoveToNextAttribute();
        }

        public override bool Read()
        {
            return _implementation.Read();
        }

        public override bool ReadAttributeValue()
        {
            return _implementation.ReadAttributeValue();
        }

        public override void ResolveEntity()
        {
            _implementation.ResolveEntity();
        }

        public override int AttributeCount => _implementation.AttributeCount;
        public override string BaseURI => _implementation.BaseURI;
        public override int Depth => _implementation.Depth;
        public override bool EOF => _implementation.EOF;
        public override bool IsEmptyElement => _implementation.IsEmptyElement;
        public override string LocalName => _implementation.LocalName;
        public override string NamespaceURI => _implementation.NamespaceURI;
        public override XmlNameTable NameTable => _implementation.NameTable;
        public override XmlNodeType NodeType => _implementation.NodeType;
        public override string Prefix => _implementation.Prefix;
        public override ReadState ReadState => _implementation.ReadState;
        public override string Value => _implementation.Value;
        public bool HasLineInfo()
        {
            return _implementation.HasLineInfo();
        }

        public int LineNumber => _implementation.LineNumber;
        public int LinePosition => _implementation.LinePosition;
    }
}
