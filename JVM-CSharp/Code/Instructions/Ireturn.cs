using JvmSharp.Java;
using JvmSharp.Java.Primitive;
using JvmSharp.Loader;
using JvmSharp.Loader.CpInfo;
using JvmSharp.Runtime;

namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        public static IObject Ireturn(ref Frame frame)
        {
            // TODO:　stop using new instance
            var intObj = ObjectFactory.Create(new IntDefinition());
            intObj.SetPrimitiveValue(frame.GetStackRef().PopInt());
            return intObj;
        }
    }
}
