using JvmSharp.Java;
using JvmSharp.Loader;
using JvmSharp.Loader.CpInfo;
using JvmSharp.Runtime;

namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        public static void Iadd(ref Frame frame)
        {
            frame.GetStackRef().Push(frame.GetStackRef().PopInt() + frame.GetStackRef().PopInt());
        }
    }
}
