namespace JvmSharp.Java
{
    internal interface IObject
    {
        uint Handle { get; }
        
        IClassDefinition Definition { get; }

        IDictionary<string, IObject> Fields { get; }
    }
}
