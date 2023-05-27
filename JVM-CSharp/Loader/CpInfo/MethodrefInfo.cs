namespace JvmSharp.Loader.CpInfo
{
    internal record MethodrefInfo(ushort ClassIndex, ushort NameAndTypeIndex) : ICpInfo
    {
        public ConstantKind Kind => ConstantKind.Methodref;

        public override string ToString() => $"#{ClassIndex}.#{NameAndTypeIndex}";
    }
}
