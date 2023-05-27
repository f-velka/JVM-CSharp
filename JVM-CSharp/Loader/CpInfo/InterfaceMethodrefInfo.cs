namespace JvmSharp.Loader.CpInfo
{
    internal record InterfaceMethodrefInfo(ushort ClassIndex, ushort NameAndTypeIndex) : ICpInfo
    {
        public ConstantKind Kind => ConstantKind.InterfaceMethodref;

        public override string ToString() => $"#{ClassIndex}.#{NameAndTypeIndex}";
    }
}
