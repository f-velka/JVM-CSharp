namespace JvmSharp.Java
{
    internal interface IClassDefinition
    {
        string FullName { get; }

        IReadOnlyList<IClassDefinition> SuperClassDefinitions { get; }

        IReadOnlyDictionary<string, IClassDefinition> FieldDefinitions { get; }

        IReadOnlyDictionary<string, IClassDefinition> StaticFieldDefinitions { get; }

        IReadOnlyDictionary<string, IObject> StaticFields { get; }

        IReadOnlySet<string> Methods { get; }

        IObject? InvokeMethod(IObject thisObj, string name, IRuntimeContext context, IObject? arg);
    }
}
