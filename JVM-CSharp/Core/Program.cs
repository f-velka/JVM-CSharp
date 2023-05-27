using JvmSharp.Loader;
using JvmSharp.Runtime;
using JvmSharp.Runner;

namespace JvmSharp.Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("need class file path");
            }
            Runner.Run(args[0]);
        }
    }
}