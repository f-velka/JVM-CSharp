namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        public static void Aload_1(ref Frame frame)
        {
            frame.GetStackRef().Push(frame.GetLocalsRef().GetUint(1));
        }
    }
}
