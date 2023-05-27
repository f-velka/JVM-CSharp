namespace JvmSharp.Loader.CpInfo
{
    internal record StringInfo(ushort StringIndex) : ICpInfo
    {
        public ConstantKind Kind => ConstantKind.String;

        public override string ToString() => $"#{StringIndex}";
    }
}
