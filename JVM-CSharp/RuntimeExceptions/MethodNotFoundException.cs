using System.Text;

namespace JvmSharp.RuntimeExceptions
{
    internal class MethodNotFoundException : Exception
    {
        public MethodNotFoundException(string message) : base(message)
        {
        }

        public MethodNotFoundException(ReadOnlySpan<byte> methodName) : base(Encoding.UTF8.GetString(methodName))
        {
        }
    }
}
