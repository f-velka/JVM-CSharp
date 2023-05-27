namespace JvmSharp.Java
{
    internal enum JavaType
    {
        Int,
        // TODO:
    }

    internal static class JavaTypeExtension
    {
        public static JavaType ToJavaType(this string typeStr) => typeStr switch
        {
            "I" => JavaType.Int,
            _ => throw new NotImplementedException(typeStr),
        };
    }
}
