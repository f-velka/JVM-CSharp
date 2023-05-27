namespace JvmSharp.Loader.CpInfo
{
    internal record ClassInfo(ushort NameIndex) : ICpInfo
    {
        public ConstantKind Kind => ConstantKind.Class;

        public override string ToString() => $"#{NameIndex}";
    }
}
