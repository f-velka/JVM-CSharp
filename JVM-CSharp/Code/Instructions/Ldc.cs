using JvmSharp.Java;
using JvmSharp.Loader;
using JvmSharp.Loader.CpInfo;
using JvmSharp.Runtime;

namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        public static void Ldc(CodeReader reader, ConstantPool cp, ref Frame frame, IRuntimeContext context)
        {
            var info = cp.GetAs<StringInfo>(reader.NextByte());
            var javaStr = context.ToJavaString(cp.GetUtf8Text(info.StringIndex));
            frame.GetStackRef().Push(javaStr.Handle);
        }
    }
}
