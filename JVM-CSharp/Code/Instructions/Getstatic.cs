using JvmSharp.Java;
using JvmSharp.Loader;
using JvmSharp.Loader.CpInfo;
using JvmSharp.Runtime;

namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        public static void Getstatic(CodeReader reader, ConstantPool cp, ref Frame frame, IRuntimeContext context)
        {
            // TODO:
            var fieldRefInfo = cp.GetAs<FieldRefInfo>(reader.NextUshort());
            var className = cp.GetUtf8Text(cp.GetAs<ClassInfo>(fieldRefInfo.ClassIndex).NameIndex);
            var nameAndTypeInfo = cp.GetAs<NameAndTypeInfo>(fieldRefInfo.NameAndTypeIndex);
            var fieldType = cp.GetUtf8Text(nameAndTypeInfo.DescriptorIndex);
            var field = context.GetStaticField(className, cp.GetUtf8Text(nameAndTypeInfo.NameIndex));
            frame.GetStackRef().Push(field.Handle);
        }
    }
}
