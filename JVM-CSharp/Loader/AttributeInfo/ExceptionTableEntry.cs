namespace JvmSharp.Loader.AttributeInfo
{
    internal record ExceptionTableEntry(ushort StartPc, ushort EndPc, ushort HandlerPc, ushort CatchType);
}
