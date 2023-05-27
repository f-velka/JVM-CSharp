using JvmSharp.Java;
using JvmSharp.Java.Primitive;
using JvmSharp.Loader;
using JvmSharp.Loader.CpInfo;
using JvmSharp.Runtime;
using System.Diagnostics;

namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        public static void Invokevirtual(CodeReader reader, ConstantPool cp, ref Frame frame, IRuntimeContext context)
        {
            // TODO:
            var methodRefInfo = cp.GetAs<MethodrefInfo>(reader.NextUshort());
            var classInfo = cp.GetAs<ClassInfo>(methodRefInfo.ClassIndex);
            var className = cp.GetUtf8Text(classInfo.NameIndex);
            var nameAndTypeInfo = cp.GetAs<NameAndTypeInfo>(methodRefInfo.NameAndTypeIndex);
            var methodName = cp.GetUtf8Text(nameAndTypeInfo.NameIndex);
            var descriptor = cp.GetUtf8Text(nameAndTypeInfo.DescriptorIndex);

            Debug.WriteLine($"=== start {methodName} ===");

            // TODO:
            if (descriptor == "()I")
            {
                var objectRef = ObjectStorage.Get(frame.GetStackRef().PopUint());
                var ret = objectRef.Definition.InvokeMethod(objectRef, methodName, context, null);
                if (ret == null)
                {
                    throw new InvalidOperationException("method must return int value");
                }
                frame.GetStackRef().Push(ret.GetPrimitiveValue<int>());
            }
            else if (descriptor == "(I)V")
            {
                var arg = frame.GetStackRef().PopInt();
                // XXX
                var boxed = ObjectFactory.Create(new IntDefinition());
                boxed.SetPrimitiveValue(arg);
                var objectRef = ObjectStorage.Get(frame.GetStackRef().PopUint());
                objectRef.Definition.InvokeMethod(objectRef, methodName, context, boxed);
            }
            else if (descriptor == "(Ljava/lang/String;)V")
            {
                var arg = ObjectStorage.Get(frame.GetStackRef().PopUint());
                var objectRef = ObjectStorage.Get(frame.GetStackRef().PopUint());
                objectRef.Definition.InvokeMethod(objectRef, methodName, context, arg);
            }
            else
            {
                throw new NotImplementedException();
            }

            Debug.WriteLine($"=== end {methodName} ===");
        }
    }
}
