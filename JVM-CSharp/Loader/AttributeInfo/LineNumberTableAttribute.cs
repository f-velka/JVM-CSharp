namespace JvmSharp.Loader.AttributeInfo
{
    internal record LineNumberTableAttribute(
        IReadOnlyList<LineNumberEntry> LineNumberTable) : IAttributeInfo
    {
        public AttributeKind Kind => AttributeKind.LineNumberTable;
    }
}
