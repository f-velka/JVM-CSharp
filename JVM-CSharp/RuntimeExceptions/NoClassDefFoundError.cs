namespace JvmSharp.RuntimeExceptions
{
    internal class NoClassDefFoundError : Exception
    {
        public NoClassDefFoundError(string className): base(className)
        {
        }
    }
}
