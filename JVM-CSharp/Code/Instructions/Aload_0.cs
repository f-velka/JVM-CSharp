namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        public static void Aload_0(ref Frame frame)
        {
            frame.GetStackRef().Push(frame.GetLocalsRef().GetUint(0));
        }
    }
}
