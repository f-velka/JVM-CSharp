namespace JvmSharp.Loader.AttributeInfo
{
    internal record CodeAttribute(
        ushort MaxStack,
        ushort MaxLocals,
        IReadOnlyList<byte> Code,
        IReadOnlyList<ExceptionTableEntry> ExceptionTable,
        IReadOnlyList<IAttributeInfo> Attributes
    ) : IAttributeInfo
    {
        public AttributeKind Kind => AttributeKind.Code;
    }
}
