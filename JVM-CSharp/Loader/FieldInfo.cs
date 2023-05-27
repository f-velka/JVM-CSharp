using JvmSharp.Loader.AttributeInfo;

namespace JvmSharp.Loader
{
    internal record FieldInfo(
        ushort AccessFlags,
        ushort NameIndex,
        ushort DescriptorIndex,
        IReadOnlyList<IAttributeInfo> Attributes);
}
