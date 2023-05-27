using JvmSharp.Java;
using JvmSharp.Loader;
using JvmSharp.Loader.CpInfo;
using JvmSharp.Runtime;

namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        public static void Getfield(CodeReader reader, ConstantPool cp, ref Frame frame, IRuntimeContext context)
        {
            var fieldRefInfo = cp.GetAs<FieldRefInfo>(reader.NextUshort());
            var className = cp.GetUtf8Text(cp.GetAs<ClassInfo>(fieldRefInfo.ClassIndex).NameIndex);
            var nameAndTypeInfo = cp.GetAs<NameAndTypeInfo>(fieldRefInfo.NameAndTypeIndex);
            var fieldType = cp.GetUtf8Text(nameAndTypeInfo.DescriptorIndex);
            var fieldName = cp.GetUtf8Text(nameAndTypeInfo.NameIndex);
            var objectRef = ObjectStorage.Get(frame.GetStackRef().PopUint());
            switch (fieldType.ToJavaType())
            {
                case JavaType.Int:
                    var val = objectRef.Fields[fieldName].GetPrimitiveValue<int>();
                    frame.GetStackRef().Push(val);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
