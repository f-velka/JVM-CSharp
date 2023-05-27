namespace JvmSharp.Loader.AttributeInfo
{
    internal record SourceFileAttribute(ushort SourceFileIndex) : IAttributeInfo
    {
        public AttributeKind Kind => AttributeKind.SourceFile;
    }
}
