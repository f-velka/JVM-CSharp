using JvmSharp.Loader;
using JvmSharp.Runner;
using JvmSharp.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JvmSharp.Core
{
    public static class Runner
    {
        public static void Run(string classFilePath)
        {
            var context = new RuntimeContext();
            var classFile = ClassLoader.Load(classFilePath, context);
            JavaRunner.Run(classFile, context);
        }
    }
}
