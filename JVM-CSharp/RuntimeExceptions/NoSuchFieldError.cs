namespace JvmSharp.RuntimeExceptions
{
    internal class NoSuchFieldError : Exception
    {
        public NoSuchFieldError(string fieldName): base(fieldName)
        {
        }
    }
}
