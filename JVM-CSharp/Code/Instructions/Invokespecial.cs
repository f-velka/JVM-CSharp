using JvmSharp.Java;
using JvmSharp.Loader;
using JvmSharp.Loader.CpInfo;
using JvmSharp.Runtime;
using JvmSharp.RuntimeExceptions;
using System.Diagnostics;

namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        public static void Invokespecial(CodeReader reader, ConstantPool cp, ref Frame frame, IRuntimeContext context)
        {
            // TODO:
            var methodRefInfo = cp.GetAs<MethodrefInfo>(reader.NextUshort());
            var classInfo = cp.GetAs<ClassInfo>(methodRefInfo.ClassIndex);
            var className = cp.GetUtf8Text(classInfo.NameIndex);
            var nameAndTypeInfo = cp.GetAs<NameAndTypeInfo>(methodRefInfo.NameAndTypeIndex);
            var methodName = cp.GetUtf8Text(nameAndTypeInfo.NameIndex);
            var descriptor = cp.GetUtf8Text(nameAndTypeInfo.DescriptorIndex);
            var objectRef = ObjectStorage.Get(frame.GetStackRef().PopUint());

            if (methodName == "<init>")
            {
                IClassDefinition def;
                if (className == objectRef.Definition.FullName)
                {
                    def = objectRef.Definition;
                }
                else
                {
                    def = objectRef.Definition.SuperClassDefinitions.First(x => x.FullName == className);

                }
                Debug.WriteLine($"=== start {methodName} ===");
                def.InvokeMethod(objectRef, methodName, context, null);
                Debug.WriteLine($"=== end {methodName} ===");
            }
            else
            {
                throw new NotImplementedException();
            }

        }
    }
}
