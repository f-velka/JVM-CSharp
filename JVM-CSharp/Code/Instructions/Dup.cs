namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        public static void Dup(ref Frame frame)
        {
            frame.GetStackRef().Dup();
        }
    }
}
