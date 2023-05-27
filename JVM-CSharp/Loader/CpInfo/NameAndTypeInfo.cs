namespace JvmSharp.Loader.CpInfo
{
    internal record NameAndTypeInfo(ushort NameIndex, ushort DescriptorIndex) : ICpInfo
    {
        public ConstantKind Kind => ConstantKind.NameAndType;

        public override string ToString() => $"#{NameIndex}:#{DescriptorIndex}";
    }
}
