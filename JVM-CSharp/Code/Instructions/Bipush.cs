namespace JvmSharp.Code.Instructions
{
    internal static partial class Instruction
    {
        internal static void Bipush(CodeReader reader, ref Frame frame)
        {
            frame.GetStackRef().Push((int)reader.NextByte());
        }
    }
}
