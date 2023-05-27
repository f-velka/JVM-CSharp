namespace JvmSharp.Loader.CpInfo
{
    internal record FieldRefInfo(ushort ClassIndex, ushort NameAndTypeIndex) : ICpInfo
    {
        public ConstantKind Kind => ConstantKind.Fieldref;

        public override string ToString() => $"#{ClassIndex}.#{NameAndTypeIndex}";
    }
}
