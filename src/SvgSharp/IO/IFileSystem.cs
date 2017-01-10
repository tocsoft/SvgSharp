using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SvgSharp.IO
{
    public interface IFileSystem
    {
        Stream Open(string path);
        bool FileExists(string path);
    }
}
