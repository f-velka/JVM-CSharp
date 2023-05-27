namespace JvmSharp.Loader.CpInfo
{
    internal abstract record RefInfo(ushort ClassIndex, ushort NameAndTypeIndex) : ICpInfo
    {
        public abstract ConstantKind Kind { get; }

        public override string ToString() => $"#{ClassIndex}.#{NameAndTypeIndex}";
    }
}
