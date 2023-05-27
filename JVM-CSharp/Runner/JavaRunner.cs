using JvmSharp.Code;
using JvmSharp.Java;
using JvmSharp.Runtime;

namespace JvmSharp.Runner
{
    internal static class JavaRunner
    {
        public static void Run(ClassFile classFile, RuntimeContext context)
        {
            CodeExecuter.ExecuteMethod(classFile.FindMethod("main"), null, classFile.Cp, context);
        }
    }
}
