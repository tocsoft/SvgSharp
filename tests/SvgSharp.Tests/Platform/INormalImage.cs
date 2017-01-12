using System.IO;

namespace ImageSharp.Tests.Platform
{
    public interface INormalImage
    {
        void Save(MemoryStream ms);
        void Save(string path);
    }
}