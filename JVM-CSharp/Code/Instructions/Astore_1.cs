namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        public static void Astore_1(ref Frame frame)
        {
            frame.GetLocalsRef().Set(1, frame.GetStackRef().PopUint());
        }
    }
}
