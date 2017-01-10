using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SvgSharp
{
    public class Configuration
    {
        private static Lazy<Configuration> lazyDefault = new Lazy<Configuration>(CreateDefault);
        public static  Configuration Default { get; set; }


        private static Configuration CreateDefault() {
            //TODO search via reflection for an implementation of IFileSystem based on the current runtime.

            return new SvgSharp.Configuration();

        }
    }
}
