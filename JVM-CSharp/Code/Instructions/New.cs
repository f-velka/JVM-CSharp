using JvmSharp.Java;
using JvmSharp.Loader;
using JvmSharp.Loader.CpInfo;
using JvmSharp.Runtime;

namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        public static void New(CodeReader reader, ConstantPool cp, ref Frame frame, IRuntimeContext context)
        {
            var classInfo = cp.GetAs<ClassInfo>(reader.NextUshort());
            var className = cp.GetUtf8Text(classInfo.NameIndex);
            var newObj = context.CreateInstance(className);
            frame.GetStackRef().Push(newObj.Handle);
        }
    }
}
