namespace JvmSharp.Loader
{
    internal enum ConstantKind
    {
        Utf8 = 1,
        Integer = 3,
        Float = 4,
        Long = 5,
        Double = 6,
        Class = 7,
        String = 8,
        Fieldref = 9,
        Methodref = 10,
        InterfaceMethodref = 11,
        NameAndType = 12,
        MethodHandle = 15,
        MethodType = 16,
        Dynamic = 17,
        InvokeDynamic = 18,
    }
}
