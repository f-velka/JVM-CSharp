namespace JvmSharp.RuntimeExceptions
{
    internal class NoSuchMethodError : Exception
    {
        public NoSuchMethodError(string methodName): base(methodName)
        {
        }
    }
}
