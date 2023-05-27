namespace JvmSharp.Code
{
    internal class CodeReader
    {
        IReadOnlyList<byte> Code { get; }

        public bool IsEnd => index == Code.Count;

        private int index;

        public CodeReader(IReadOnlyList<byte> code)
        {
            Code = code;
        }

        public byte NextByte() => Code[index++];

        public ushort NextUshort() => (ushort)((NextByte() << 8) | NextByte());
    }
}
