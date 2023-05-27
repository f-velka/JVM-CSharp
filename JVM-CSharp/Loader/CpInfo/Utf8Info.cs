namespace JvmSharp.Loader.CpInfo
{
    internal record Utf8Info(IReadOnlyList<byte> Bytes, string Text) : ICpInfo
    {
        public ConstantKind Kind => ConstantKind.Utf8;

        private byte[]? utf8Text;
        public ReadOnlySpan<byte> Utf8Text
        {
            get
            {
                utf8Text ??= Bytes.ToArray();
                return utf8Text.AsSpan();
            }
        }

        public override string ToString() => Text;
    }
}
