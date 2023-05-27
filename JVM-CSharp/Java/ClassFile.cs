using JvmSharp.Loader;
using JvmSharp.Loader.AttributeInfo;
using JvmSharp.RuntimeExceptions;

namespace JvmSharp.Java
{
    internal record ClassFile(
        byte[] Magic,
        ushort MinorVersion,
        ushort MajorVersion,
        ConstantPool Cp,
        ushort AccessFlags,
        ushort ThisClass,
        ushort SuperClass,
        IReadOnlyList<ushort> Interfaces,
        IReadOnlyList<FieldInfo> Fields,
        IReadOnlyList<MethodInfo> Methods,
        IReadOnlyList<IAttributeInfo> Attributes
    )
    {
        public MethodInfo FindMethod(string name)
        {
            foreach (var method in Methods)
            {
                if (Cp.GetUtf8Text(method.NameIndex) == name)
                    return method;
            }
            throw new MethodNotFoundException(name);
        }
    };
}
