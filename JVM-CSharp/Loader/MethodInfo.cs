using JvmSharp.Loader.AttributeInfo;
using JvmSharp.RuntimeExceptions;

namespace JvmSharp.Loader
{
    internal record MethodInfo(
        ushort AccessFlags,
        ushort NameIndex,
        ushort DescriptorIndex,
        IReadOnlyList<IAttributeInfo> Attributes
    )
    {
        public T FindAttribute<T>() where T : IAttributeInfo
        {
            return Attributes.OfType<T>().FirstOrDefault()
                ?? throw new AttributeNotFoundException(nameof(T));
        }
    }
}
