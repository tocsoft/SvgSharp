using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Svg.Exceptions
{
	public class SvgMemoryException : Exception
	{
		public SvgMemoryException() {}
		public SvgMemoryException(string message) : base(message) {}
		public SvgMemoryException(string message, Exception inner) : base(message, inner) {}
	}
}
